using System;
using System.Numerics;

namespace BulletSharp
{
    public class KinematicCharacterController : ICharacterController, IDisposable
    {
        protected float m_halfHeight;

        protected PairCachingGhostObject m_ghostObject;
        protected ConvexShape m_convexShape; //is also in m_ghostObject, but it needs to be convex, so we store it here to avoid upcast

        protected float m_maxPenetrationDepth;
        protected float m_verticalVelocity;
        protected float m_verticalOffset;

        protected float m_fallSpeed;
        protected float m_jumpSpeed;
        protected float m_SetjumpSpeed;
        protected float m_maxJumpHeight;
        protected float m_maxSlopeRadians; // Slope angle that is set (used for returning the exact value)
        protected float m_maxSlopeCosine;  // Cosine equivalent of m_maxSlopeRadians (calculated once when set, for optimization)
        protected float m_gravity;

        protected float m_turnAngle;

        protected float m_stepHeight;

        protected float m_addedMargin; //@to do: remove this and fix the code

        ///this is the desired walk direction, set by the user
        protected Vector3 m_walkDirection;
        protected Vector3 m_normalizedDirection;
        protected Vector3 m_AngVel;

        protected Vector3 m_jumpPosition;

        //some internal variables
        protected Vector3 m_currentPosition;
        protected float m_currentStepOffset;
        protected Vector3 m_targetPosition;

        protected Quaternion m_currentOrientation;
        protected Quaternion m_targetOrientation;

        ///keep track of the contact manifolds
        protected AlignedManifoldArray m_manifoldArray = new AlignedManifoldArray();

        protected bool m_touchingContact;
        protected Vector3 m_touchingNormal;

        protected float m_linearDamping;
        protected float m_angularDamping;

        protected bool m_wasOnGround;
        protected bool m_wasJumping;
        protected bool m_useGhostObjectSweepTest;
        protected bool m_useWalkDirection;
        protected float m_velocityTimeInterval;
        protected Vector3 m_up;
        protected Vector3 m_jumpAxis;

        protected bool m_interpolateUp;
        protected bool full_drop;
        protected bool bounce_fix;

        protected static Vector3 GetNormalizedVector(ref Vector3 v)
        {
            if (v.Length() < MathUtil.SIMD_EPSILON)
            {
                return Vector3.Zero;
            }
            return Vector3.Normalize(v);
        }

        protected Vector3 ComputeReflectionDirection(ref Vector3 direction, ref Vector3 normal)
        {
            float dot = Vector3.Dot(direction, normal);
            return direction - (2.0f * dot) * normal;
        }

        protected Vector3 ParallelComponent(ref Vector3 direction, ref Vector3 normal)
        {
            float magnitude = Vector3.Dot(direction, normal);
            return normal * magnitude;
        }

        protected Vector3 PerpindicularComponent(ref Vector3 direction, ref Vector3 normal)
        {
            return direction - ParallelComponent(ref direction, ref normal);
        }

        protected bool RecoverFromPenetration(CollisionWorld collisionWorld)
        {
            // Here we must refresh the overlapping paircache as the penetrating movement itself or the
            // previous recovery iteration might have used setWorldTransform and pushed us into an object
            // that is not in the previous cache contents from the last timestep, as will happen if we
            // are pushed into a new AABB overlap. Unhandled this means the next convex sweep gets stuck.
            //
            // Do this by calling the broadphase's setAabb with the moved AABB, this will update the broadphase
            // paircache and the ghostobject's internal paircache at the same time.    /BW

            Vector3 minAabb, maxAabb;
            m_convexShape.GetAabb(m_ghostObject.WorldTransform, out minAabb, out maxAabb);
            collisionWorld.Broadphase.SetAabbRef(m_ghostObject.BroadphaseHandle,
                         ref minAabb,
                         ref maxAabb,
                         collisionWorld.Dispatcher);

            bool penetration = false;

            collisionWorld.Dispatcher.DispatchAllCollisionPairs(m_ghostObject.OverlappingPairCache, collisionWorld.DispatchInfo, collisionWorld.Dispatcher);

            m_currentPosition = m_ghostObject.WorldTransform.Translation;

            //  btScalar maxPen = btScalar(0.0);
            for (int i = 0; i < m_ghostObject.OverlappingPairCache.NumOverlappingPairs; i++)
            {
                m_manifoldArray.Clear();

                BroadphasePair collisionPair = m_ghostObject.OverlappingPairCache.OverlappingPairArray[i];

                CollisionObject obj0 = collisionPair.Proxy0.ClientObject as CollisionObject;
                CollisionObject obj1 = collisionPair.Proxy1.ClientObject as CollisionObject;

                if ((obj0 != null && !obj0.HasContactResponse) || (obj1 != null && !obj1.HasContactResponse))
                    continue;

                if (!NeedsCollision(obj0, obj1))
                    continue;

                var collisionAlgorithm = collisionPair.Algorithm;
                if (collisionAlgorithm != null)
                    collisionAlgorithm.GetAllContactManifolds(m_manifoldArray);

                for (int j = 0; j < m_manifoldArray.Count; j++)
                {
                    PersistentManifold manifold = m_manifoldArray[j];
                    float directionSign = manifold.Body0 == m_ghostObject ? -1.0f : 1.0f;
                    for (int p = 0; p < manifold.NumContacts; p++)
                    {
                        ManifoldPoint pt = manifold.GetContactPoint(p);

                        float dist = pt.Distance;

                        if (dist < -m_maxPenetrationDepth)
                        {
                            // to do: cause problems on slopes, not sure if it is needed
                            //if (dist < maxPen)
                            //{
                            //  maxPen = dist;
                            //  m_touchingNormal = pt.m_normalWorldOnB * directionSign;//??

                            //}
                            m_currentPosition += pt.NormalWorldOnB * (directionSign * dist * 0.2f);
                            penetration = true;
                        }
                        else
                        {
                            //System.Console.WriteLine("touching " + dist);
                        }
                    }

                    //manifold.ClearManifold();
                }
            }
            Matrix4x4 newTrans = m_ghostObject.WorldTransform;
            newTrans.Translation = m_currentPosition;
            m_ghostObject.WorldTransform = newTrans;
            //System.Console.WriteLine("m_touchingNormal = " + m_touchingNormal);
            return penetration;
        }

        protected void StepUp(CollisionWorld world)
        {
            float stepHeight = 0.0f;
            if (m_verticalVelocity < 0.0f)
                stepHeight = m_stepHeight;

            // phase 1: up
            Matrix4x4 start, end;

            start = Matrix4x4.Identity;
            end = Matrix4x4.Identity;

            /* FIX ME: Handle penetration properly */
            start.Translation = m_currentPosition;

            m_targetPosition = m_currentPosition + m_up * (stepHeight) + m_jumpAxis * ((m_verticalOffset > 0f ? m_verticalOffset : 0f));
            m_currentPosition = m_targetPosition;

            end.Translation = m_targetPosition;

            start.SetRotation(m_currentOrientation, out start);
            end.SetRotation(m_targetOrientation, out end);

            using (KinematicClosestNotMeConvexResultCallback callback = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, -m_up, m_maxSlopeCosine))
            {
                callback.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
                callback.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

                if (m_useGhostObjectSweepTest)
                {
                    m_ghostObject.ConvexSweepTest(m_convexShape, start, end, callback, world.DispatchInfo.AllowedCcdPenetration);
                }
                else
                {
                    world.ConvexSweepTest(m_convexShape, start, end, callback, world.DispatchInfo.AllowedCcdPenetration);
                }

                if (callback.HasHit && m_ghostObject.HasContactResponse && NeedsCollision(m_ghostObject, callback.HitCollisionObject))
                {
                    // Only modify the position if the hit was a slope and not a wall or ceiling.
                    if (Vector3.Dot(callback.HitNormalWorld, m_up) > 0.0)
                    {
                        // we moved up only a fraction of the step height
                        m_currentStepOffset = stepHeight * callback.ClosestHitFraction;
                        if (m_interpolateUp == true)
                            m_currentPosition = Vector3.Lerp(m_currentPosition, m_targetPosition, callback.ClosestHitFraction);
                        else
                            m_currentPosition = m_targetPosition;
                    }

                    Matrix4x4 xform = m_ghostObject.WorldTransform;
                    xform.Translation = m_currentPosition;
                    m_ghostObject.WorldTransform = xform;

                    // fix penetration if we hit a ceiling for example
                    int numPenetrationLoops = 0;
                    m_touchingContact = false;
                    while (RecoverFromPenetration(world))
                    {
                        numPenetrationLoops++;
                        m_touchingContact = true;
                        if (numPenetrationLoops > 4)
                        {
                            //System.Console.WriteLine("character could not recover from penetration = " + numPenetrationLoops);
                            break;
                        }
                    }
                    m_targetPosition = m_ghostObject.WorldTransform.Translation;
                    m_currentPosition = m_targetPosition;

                    if (m_verticalOffset > 0)
                    {
                        m_verticalOffset = 0.0f;
                        m_verticalVelocity = 0.0f;
                        m_currentStepOffset = m_stepHeight;
                    }
                }
                else
                {
                    m_currentStepOffset = stepHeight;
                    m_currentPosition = m_targetPosition;
                }
            }
        }
        protected void UpdateTargetPositionBasedOnCollision(ref Vector3 hitNormal, float tangentMag = 0f, float normalMag = 1f)
        {
            Vector3 movementDirection = m_targetPosition - m_currentPosition;
            float movementLength = movementDirection.Length();
            if (movementLength > MathUtil.SIMD_EPSILON)
            {
                movementDirection = Vector3.Normalize(movementDirection);

                Vector3 reflectDir = ComputeReflectionDirection(ref movementDirection, ref hitNormal);
                reflectDir = Vector3.Normalize(reflectDir);

                Vector3 parallelDir, perpindicularDir;

                parallelDir = ParallelComponent(ref reflectDir, ref hitNormal);
                perpindicularDir = PerpindicularComponent(ref reflectDir, ref hitNormal);

                m_targetPosition = m_currentPosition;
                if (false) //tangentMag != 0.0)
                {
                    //Vector3 parComponent = parallelDir * (tangentMag * movementLength);
                    //System.Console.WriteLine("parComponent=" + parComponent);
                    //m_targetPosition += parComponent;
                }

                if (normalMag != 0.0f)
                {
                    Vector3 perpComponent = perpindicularDir * (normalMag * movementLength);
                    //System.Console.WriteLine("perpComponent=" + perpComponent);
                    m_targetPosition += perpComponent;
                }
            }
            else
            {
                //System.Console.WriteLine("movementLength don't normalize a zero vector");
            }
        }

        protected void StepForwardAndStrafe(CollisionWorld collisionWorld, ref Vector3 walkMove)
        {
            //System.Console.WriteLine("m_normalizedDirection=" + m_normalizedDirection);
            // phase 2: forward and strafe
            Matrix4x4 start = Matrix4x4.Identity;
            Matrix4x4 end = Matrix4x4.Identity;

            m_targetPosition = m_currentPosition + walkMove;

            float fraction = 1.0f;
            float distance2 = (m_currentPosition - m_targetPosition).LengthSquared();
            //System.Console.WriteLine("distance2=" + distance2);

            int maxIter = 10;

            while (fraction > 0.01f && maxIter-- > 0)
            {
                start.Translation = m_currentPosition;
                end.Translation = m_targetPosition;
                Vector3 sweepDirNegative = m_currentPosition - m_targetPosition;

                start.SetRotation(m_currentOrientation, out start);
                end.SetRotation(m_targetOrientation, out end);

                using (KinematicClosestNotMeConvexResultCallback callback = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, sweepDirNegative, 0.0f))
                {
                    callback.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
                    callback.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

                    float margin = m_convexShape.Margin;
                    m_convexShape.Margin = margin + m_addedMargin;

                    if (start != end)
                    {
                        if (m_useGhostObjectSweepTest)
                        {
                            m_ghostObject.ConvexSweepTest(m_convexShape, start, end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                        }
                        else
                        {
                            collisionWorld.ConvexSweepTest(m_convexShape, start, end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                        }
                    }
                    m_convexShape.Margin = margin;

                    fraction -= callback.ClosestHitFraction;

                    if (callback.HasHit && GhostObject.HasContactResponse && NeedsCollision(m_ghostObject, callback.HitCollisionObject))
                    {
                        // we moved only a fraction
                        //float hitDistance = (callback.HitPointWorld - m_currentPosition).Length;

                        //Vector3.Lerp(ref m_currentPosition, ref m_targetPosition, callback.ClosestHitFraction, out m_currentPosition);
                        Vector3 hitNormalWorld = callback.HitNormalWorld;
                        UpdateTargetPositionBasedOnCollision(ref hitNormalWorld);
                        Vector3 currentDir = m_targetPosition - m_currentPosition;
                        distance2 = currentDir.LengthSquared();
                        if (distance2 > MathUtil.SIMD_EPSILON)
                        {
                            currentDir = Vector3.Normalize(currentDir);
                            /* See Quake2: "If velocity is against original velocity, stop ead to avoid tiny oscilations in sloping corners." */
                            if (Vector3.Dot(currentDir, m_normalizedDirection) <= 0.0f)
                            {
                                break;
                            }
                        }
                        else
                        {
                            //System.Console.WriteLine("currentDir: don't normalize a zero vector");
                            break;
                        }
                    }
                    else
                    {
                        m_currentPosition = m_targetPosition;
                    }
                }
            }
        }
        protected void StepDown(CollisionWorld collisionWorld, float dt)
        {
            Matrix4x4 start, end, end_double;
            bool runonce = false;

            // phase 3: down
            /*float additionalDownStep = (m_wasOnGround && !OnGround) ? m_stepHeight : 0;
            Vector3 step_drop = m_up * (m_currentStepOffset + additionalDownStep);
            float downVelocity = (additionalDownStep == 0.0 && m_verticalVelocity < 0 ? -m_verticalVelocity : 0) * dt;
            Vector3 gravity_drop = m_up * downVelocity;
            m_targetPosition -= (step_drop + gravity_drop);*/

            Vector3 orig_position = m_targetPosition;

            float downVelocity = (m_verticalVelocity < 0f ? -m_verticalVelocity : 0f) * dt;

            if (m_verticalVelocity > 0.0)
                return;

            if (downVelocity > 0.0 && downVelocity > m_fallSpeed && (m_wasOnGround || !m_wasJumping))
                downVelocity = m_fallSpeed;

            Vector3 step_drop = m_up * (m_currentStepOffset + downVelocity);
            m_targetPosition -= step_drop;

            using (KinematicClosestNotMeConvexResultCallback callback = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, m_up, m_maxSlopeCosine))
            using (KinematicClosestNotMeConvexResultCallback callback2 = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, m_up, m_maxSlopeCosine))
            {
                callback.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
                callback.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

                callback2.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
                callback2.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

                while (true)
                {
                    start = Matrix4x4.CreateTranslation(m_currentPosition);
                    end = Matrix4x4.CreateTranslation(m_targetPosition);

                    start.SetRotation(m_currentOrientation, out start);
                    end.SetRotation(m_targetOrientation, out end);

                    //set double test for 2x the step drop, to check for a large drop vs small drop
                    end_double = Matrix4x4.CreateTranslation(m_targetPosition - step_drop);

                    if (m_useGhostObjectSweepTest)
                    {
                        m_ghostObject.ConvexSweepTest(m_convexShape, start, end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);

                        if (!callback.HasHit && m_ghostObject.HasContactResponse)
                        {
                            //test a double fall height, to see if the character should interpolate it's fall (full) or not (partial)
                            m_ghostObject.ConvexSweepTest(m_convexShape, start, end_double, callback2, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                        }
                    }
                    else
                    {
                        collisionWorld.ConvexSweepTest(m_convexShape, start, end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);

                        if (!callback.HasHit && m_ghostObject.HasContactResponse)
                        {
                            //test a double fall height, to see if the character should interpolate it's fall (large) or not (small)
                            collisionWorld.ConvexSweepTest(m_convexShape, start, end_double, callback2, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                        }
                    }

                    float downVelocity2 = (m_verticalVelocity < 0f ? -m_verticalVelocity : 0f) * dt;
                    bool has_hit;
                    if (bounce_fix == true)
                        has_hit = (callback.HasHit || callback2.HasHit) && m_ghostObject.HasContactResponse && NeedsCollision(m_ghostObject, callback.HitCollisionObject);
                    else
                        has_hit = callback2.HasHit && m_ghostObject.HasContactResponse && NeedsCollision(m_ghostObject, callback2.HitCollisionObject);

                    float stepHeight = 0.0f;
                    if (m_verticalVelocity < 0.0)
                        stepHeight = m_stepHeight;

                    if (downVelocity2 > 0.0 && downVelocity2 < stepHeight && has_hit == true && runonce == false && (m_wasOnGround || !m_wasJumping))
                    {
                        //redo the velocity calculation when falling a small amount, for fast stairs motion
                        //for larger falls, use the smoother/slower interpolated movement by not touching the target position

                        m_targetPosition = orig_position;
                        downVelocity = stepHeight;

                        step_drop = m_up * (m_currentStepOffset + downVelocity);
                        m_targetPosition -= step_drop;
                        runonce = true;
                        continue;  //re-run previous tests
                    }
                    break;
                }

                if ((m_ghostObject.HasContactResponse && (callback.HasHit && NeedsCollision(m_ghostObject, callback.HitCollisionObject))) || runonce == true)
                {
                    // we dropped a fraction of the height -> hit floor
                    float fraction = (m_currentPosition.Y - callback.HitPointWorld.Y) / 2;

                    //System.Console.WriteLine("hitpoint: {0} - pos {1}", callback.HitPointWorld.Y, m_currentPosition.Y);

                    if (bounce_fix == true)
                    {
                        if (full_drop == true)
                            m_currentPosition = Vector3.Lerp(m_currentPosition, m_targetPosition, callback.ClosestHitFraction);
                        else
                            //due to errors in the closestHitFraction variable when used with large polygons, calculate the hit fraction manually
                            m_currentPosition = Vector3.Lerp(m_currentPosition, m_targetPosition, fraction);
                    }
                    else
                        m_currentPosition = Vector3.Lerp(m_currentPosition, m_targetPosition, callback.ClosestHitFraction);

                    full_drop = false;

                    m_verticalVelocity = 0.0f;
                    m_verticalOffset = 0.0f;
                    m_wasJumping = false;
                }
                else
                {
                    // we dropped the full height

                    full_drop = true;

                    if (bounce_fix == true)
                    {
                        downVelocity = (m_verticalVelocity < 0f ? -m_verticalVelocity : 0f) * dt;
                        if (downVelocity > m_fallSpeed && (m_wasOnGround || !m_wasJumping))
                        {
                            m_targetPosition += step_drop;  //undo previous target change
                            downVelocity = m_fallSpeed;
                            step_drop = m_up * (m_currentStepOffset + downVelocity);
                            m_targetPosition -= step_drop;
                        }
                    }
                    //System.Console.WriteLine("full drop - {0}, {1}", m_currentPosition.Y, m_targetPosition.Y);

                    m_currentPosition = m_targetPosition;
                }
            }
        }

        protected virtual bool NeedsCollision(CollisionObject body0, CollisionObject body1)
        {
            bool collides = (body0.BroadphaseHandle.CollisionFilterGroup & body1.BroadphaseHandle.CollisionFilterMask) != 0;
            collides = collides && (body1.BroadphaseHandle.CollisionFilterGroup & body0.BroadphaseHandle.CollisionFilterMask) != 0;
            return collides;
        }

        protected void SetUpVector(ref Vector3 up)
        {
            if (m_up == up)
                return;

            Vector3 u = m_up;

            if (up.LengthSquared() > 0)
                m_up = Vector3.Normalize(up);
            else
                m_up = Vector3.Zero;

            if (m_ghostObject == null) return;
            Quaternion rot = GetRotation(ref m_up, ref u);

            //set orientation with new up
            Matrix4x4 xform;
            xform = m_ghostObject.WorldTransform;
            Quaternion orn = Quaternion.Inverse(rot) * xform.GetRotation();
            xform.SetRotation(orn, out xform);
            m_ghostObject.WorldTransform = xform;
        }

        protected Quaternion GetRotation(ref Vector3 v0, ref Vector3 v1)
        {
            if (v0.LengthSquared() == 0.0f || v1.LengthSquared() == 0.0f)
            {
                Quaternion q = new Quaternion();
                return q;
            }

            return MathUtil.ShortestArcQuat(ref v0, ref v1);
        }

        public KinematicCharacterController(PairCachingGhostObject ghostObject, ConvexShape convexShape, float stepHeight, ref Vector3 up)
        {
            m_ghostObject = ghostObject;
            m_jumpAxis = new Vector3(0.0f, 0.0f, 1.0f);
            m_addedMargin = 0.02f;
            m_useGhostObjectSweepTest = true;
            m_convexShape = convexShape;
            m_useWalkDirection = true; // use walk direction by default, legacy behavior
            m_gravity = 9.8f * 3.0f; // 3G acceleration.
            m_fallSpeed = 55.0f;       // Terminal velocity of a sky diver in m/s.
            m_jumpSpeed = 10.0f;       // ?
            m_SetjumpSpeed = m_jumpSpeed;
            m_interpolateUp = true;
            m_maxPenetrationDepth = 0.2f;

            Up = up;
            StepHeight = stepHeight;
            MaxSlope = MathUtil.DegToRadians(45.0f);
        }

        // IAction interface
        public virtual void UpdateAction(CollisionWorld collisionWorld, float deltaTime)
        {
            PreStep(collisionWorld);
            PlayerStep(collisionWorld, deltaTime);
        }

        // IAction interface
        public void DebugDraw(DebugDraw debugDrawer)
        {
        }

        public Vector3 Up
        {
            get => m_up;
            set
            {
                if (value.LengthSquared() > 0 && m_gravity > 0.0f)
                {
                    Gravity = -m_gravity * Vector3.Normalize(value);
                    return;
                }

                SetUpVector(ref value);
            }
        }

        /// <summary>
        /// This should probably be called setPositionIncrementPerSimulatorStep.
        /// This is neither a direction nor a velocity, but the amount to
        /// increment the position each simulation iteration, regardless
        /// of dt.
        /// This call will reset any velocity set by setVelocityForTimeInterval().
        /// </summary>
        public virtual void SetWalkDirection(Vector3 walkDirection) => SetWalkDirection(ref walkDirection);

        public virtual void SetWalkDirection(ref Vector3 walkDirection)
        {
            m_useWalkDirection = true;
            m_walkDirection = walkDirection;
            m_normalizedDirection = GetNormalizedVector(ref m_walkDirection);
        }

        /// <summary>
        /// Caller provides a velocity with which the character should move for
        /// the given time period.  After the time period, velocity is reset
        /// to zero.
        /// This call will reset any walk direction set by setWalkDirection().
        /// Negative time intervals will result in no motion.
        /// </summary>
        public void SetVelocityForTimeInterval(Vector3 velocity, float timeInterval) => SetVelocityForTimeInterval(ref velocity, timeInterval);

        public virtual void SetVelocityForTimeInterval(ref Vector3 velocity, float timeInterval)
        {
            //System.Console.WriteLine("SetVelocity!");
            //System.Console.WriteLine("  interval: " + timeInterval);
            //System.Console.WriteLine("  velocity: " + velocity);

            m_useWalkDirection = false;
            m_walkDirection = velocity;
            m_normalizedDirection = GetNormalizedVector(ref m_walkDirection);
            m_velocityTimeInterval = timeInterval;
        }

        public virtual Vector3 AngularVelocity
        {
            get => m_AngVel;
            set => m_AngVel = value;
        }

        public virtual Vector3 LinearVelocity
        {
            get => m_walkDirection + (m_verticalVelocity * m_up);
            set
            {
                Vector3 velocity = value;
                m_walkDirection = velocity;

                // HACK: if we are moving in the direction of the up, treat it as a jump :(
                if (m_walkDirection.LengthSquared() > 0)
                {
                    Vector3 w = Vector3.Normalize(velocity);
                    float c = Vector3.Dot(w, m_up);
                    if (c != 0)
                    {
                        //there is a component in walkdirection for vertical velocity
                        Vector3 upComponent = m_up * ((float)System.Math.Sin(MathUtil.SIMD_HALF_PI - System.Math.Acos(c)) * m_walkDirection.Length());
                        m_walkDirection -= upComponent;
                        m_verticalVelocity = (c < 0.0f ? -1 : 1) * upComponent.Length();

                        if (c > 0.0f)
                        {
                            m_wasJumping = true;
                            m_jumpPosition = m_ghostObject.WorldTransform.Translation;
                        }
                    }
                }
                else
                    m_verticalVelocity = 0.0f;
            }
        }

        public float LinearDamping
        {
            get => m_linearDamping;
            set => m_linearDamping = value > 1f ? 1f : value < 0f ? 0f : value;
        }

        public float AngularDamping
        {
            get => m_angularDamping;
            set => m_angularDamping = value > 1f ? 1f : value < 0f ? 0f : value;
        }

        public void Reset(CollisionWorld collisionWorld)
        {
            m_verticalVelocity = 0.0f;
            m_verticalOffset = 0.0f;
            m_wasOnGround = false;
            m_wasJumping = false;
            m_walkDirection = Vector3.Zero;
            m_velocityTimeInterval = 0.0f;

            //clear pair cache
            HashedOverlappingPairCache cache = m_ghostObject.OverlappingPairCache;
            while (cache.OverlappingPairArray.Count > 0)
            {
                BroadphasePair collisionPair = cache.OverlappingPairArray[0];
                cache.RemoveOverlappingPair(collisionPair.Proxy0, collisionPair.Proxy1, collisionWorld.Dispatcher);
            }
        }

        public void Warp(ref Vector3 origin)
        {
            Matrix4x4 xform;
            xform = Matrix4x4.Identity;
            xform.Translation = origin;
            m_ghostObject.WorldTransform = xform;
        }

        public void PreStep(CollisionWorld collisionWorld)
        {
            m_currentPosition = m_ghostObject.WorldTransform.Translation;
            m_targetPosition = m_currentPosition;

            Matrix4x4.Decompose(m_ghostObject.WorldTransform, out _, out m_currentOrientation, out _);
            m_targetOrientation = m_currentOrientation;
            //System.Console.WriteLine("m_targetPosition=" + m_targetPosition);
        }

        public void PlayerStep(CollisionWorld collisionWorld, float dt)
        {
            //System.Console.WriteLine("PlayerStep():");
            //System.Console.WriteLine("  dt = " + dt);

            if (m_AngVel.LengthSquared() > 0.0f)
            {
                m_AngVel *= (float)System.Math.Pow(1f - m_angularDamping, dt);
            }

            Matrix4x4 xform;
            // integrate for angular velocity
            if (m_AngVel.LengthSquared() > 0.0f)
            {
                xform = m_ghostObject.WorldTransform;

                Quaternion rot = new Quaternion(Vector3.Normalize(m_AngVel), m_AngVel.Length() * dt);

                Quaternion orn = rot * xform.GetRotation();

                xform.SetRotation(orn, out xform);
                m_ghostObject.WorldTransform = xform;

                m_currentPosition = m_ghostObject.WorldTransform.Translation;
                m_targetPosition = m_currentPosition;
                m_currentOrientation = m_ghostObject.WorldTransform.GetRotation();
                m_targetOrientation = m_currentOrientation;
            }

            // quick check...
            if (!m_useWalkDirection && (m_velocityTimeInterval <= 0.0 || MathUtil.FuzzyZero(m_walkDirection)))
            {
                //System.Console.WriteLine();
                return;  // no motion
            }

            m_wasOnGround = OnGround;

            //btVector3 lvel = m_walkDirection;
            //btScalar c = 0.0f;

            if (m_walkDirection.LengthSquared() > 0)
            {
                // apply damping
                m_walkDirection *= (float)System.Math.Pow(1f - m_linearDamping, dt);
            }

            m_verticalVelocity *= (float)System.Math.Pow(1f - m_linearDamping, dt);

            // Update fall velocity.
            m_verticalVelocity -= m_gravity * dt;
            if (m_verticalVelocity > 0.0 && m_verticalVelocity > m_jumpSpeed)
            {
                m_verticalVelocity = m_jumpSpeed;
            }
            if (m_verticalVelocity < 0.0 && (m_verticalVelocity < 0 ? -m_verticalVelocity : m_verticalVelocity) > (m_fallSpeed < 0 ? -m_fallSpeed : m_fallSpeed))
            {
                m_verticalVelocity = -(m_fallSpeed < 0 ? -m_fallSpeed : m_fallSpeed);
            }
            m_verticalOffset = m_verticalVelocity * dt;

            xform = m_ghostObject.WorldTransform;

            //System.Console.WriteLine("walkDirection=" + m_walkDirection);
            //System.Console.WriteLine("walkSpeed=" + walkSpeed);

            StepUp(collisionWorld);
            //todo: Experimenting with behavior of controller when it hits a ceiling..
            //bool hitUp = stepUp (collisionWorld);
            //if (hitUp)
            //{
            //  m_verticalVelocity -= m_gravity * dt;
            //  if (m_verticalVelocity > 0.0 && m_verticalVelocity > m_jumpSpeed)
            //  {
            //      m_verticalVelocity = m_jumpSpeed;
            //  }
            //  if (m_verticalVelocity < 0.0 && btFabs(m_verticalVelocity) > btFabs(m_fallSpeed))
            //  {
            //      m_verticalVelocity = -btFabs(m_fallSpeed);
            //  }
            //  m_verticalOffset = m_verticalVelocity * dt;

            //  xform = m_ghostObject->getWorldTransform();
            //}

            if (m_useWalkDirection)
            {
                StepForwardAndStrafe(collisionWorld, ref m_walkDirection);
            }
            else
            {
                //System.Console.WriteLine("  time: " + m_velocityTimeInterval);
                // still have some time left for moving!
                float dtMoving =
                    (dt < m_velocityTimeInterval) ? dt : m_velocityTimeInterval;
                m_velocityTimeInterval -= dt;

                // how far will we move while we are moving?
                Vector3 move = m_walkDirection * dtMoving;

                //System.Console.WriteLine("  dtMoving: " + dtMoving);

                // okay, step
                StepForwardAndStrafe(collisionWorld, ref move);
            }
            StepDown(collisionWorld, dt);

            //to do: Experimenting with max jump height
            //if (m_wasJumping)
            //{
            //  btScalar ds = m_currentPosition[m_upAxis] - m_jumpPosition[m_upAxis];
            //  if (ds > m_maxJumpHeight)
            //  {
            //      // substract the overshoot
            //      m_currentPosition[m_upAxis] -= ds - m_maxJumpHeight;

            //      // max height was reached, so potential energy is at max
            //      // and kinematic energy is 0, thus velocity is 0.
            //      if (m_verticalVelocity > 0.0)
            //          m_verticalVelocity = 0.0;
            //  }
            //}
            //System.Console.WriteLine();

            xform.Translation = m_currentPosition;
            m_ghostObject.WorldTransform = xform;

            int numPenetrationLoops = 0;
            m_touchingContact = false;
            while (RecoverFromPenetration(collisionWorld))
            {
                numPenetrationLoops++;
                m_touchingContact = true;
                if (numPenetrationLoops > 4)
                {
                    //System.Console.WriteLine("character could not recover from penetration, numPenetrationLoops=" + numPenetrationLoops);
                    break;
                }
            }
        }

        public float StepHeight
        {
            get => m_stepHeight;
            set => m_stepHeight = value;
        }

        public float FallSpeed
        {
            get => m_fallSpeed;
            set => m_fallSpeed = value;
        }

        public float JumpSpeed
        {
            get => m_jumpSpeed;
            set => m_SetjumpSpeed = m_jumpSpeed = value;
        }

        public void SetMaxJumpHeight(float maxJumpHeight) => m_maxJumpHeight = maxJumpHeight;
        public bool CanJump => OnGround;

        public void Jump()
        {
            m_jumpSpeed = m_SetjumpSpeed;
            m_verticalVelocity = m_jumpSpeed;
            m_wasJumping = true;

            m_jumpAxis = m_up;

            m_jumpPosition = m_ghostObject.WorldTransform.Translation;
        }

        public void Jump(ref Vector3 dir)
        {
            m_jumpSpeed = dir.Length();
            m_verticalVelocity = m_jumpSpeed;
            m_wasJumping = true;

            m_jumpAxis = Vector3.Normalize(dir);

            m_jumpPosition = m_ghostObject.WorldTransform.Translation;

#if false
            // currently no jumping.
            Matrix xform;
            m_rigidBody.getMotionState()->getWorldTransform (xform);
            Vector3 up = xform.Basis[1];
            up.Normalize();
            float magnitude = (btScalar(1.0)/m_rigidBody->getInvMass()) * 8.0f;
            m_rigidBody->applyCentralImpulse (up * magnitude);
#endif
        }

        /// <summary>
        /// Calls Jump()
        /// </summary>
        public void ApplyImpulse(ref Vector3 dir) => Jump(ref dir);

        public Vector3 Gravity
        {
            get => -m_gravity * m_up;
            set
            {
                Vector3 gravity = value;
                m_gravity = gravity.Length();
                if (gravity.LengthSquared() > 0)
                {
                    gravity = -gravity;
                    SetUpVector(ref gravity);
                }
            }
        }

        /// <summary>
        /// The max slope determines the maximum angle that the controller can walk up.
        /// The slope angle is measured in radians.
        /// </summary>
        public float MaxSlope
        {
            get => m_maxSlopeRadians;
            set
            {
                m_maxSlopeRadians = value;
                m_maxSlopeCosine = (float)System.Math.Cos(value);
            }
        }

        public float MaxPenetrationDepth
        {
            get => m_maxPenetrationDepth;
            set => m_maxPenetrationDepth = value;
        }

        public PairCachingGhostObject GhostObject => m_ghostObject;

        public void SetUseGhostSweepTest(bool useGhostObjectSweepTest)
        {
            m_useGhostObjectSweepTest = useGhostObjectSweepTest;
        }

        public bool OnGround => System.Math.Abs(m_verticalVelocity) < MathUtil.SIMD_EPSILON && System.Math.Abs(m_verticalOffset) < MathUtil.SIMD_EPSILON;

        public void SetUpInterpolate(bool v)
        {
            m_interpolateUp = v;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_manifoldArray.Dispose();
            }
        }

        ~KinematicCharacterController()
        {
           Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    ///@todo Interact with dynamic objects,
    ///Ride kinematicly animated platforms properly
    ///More realistic (or maybe just a config option) falling
    /// -> Should integrate falling velocity manually and use that in stepDown()
    ///Support jumping
    ///Support ducking
    public class KinematicClosestNotMeRayResultCallback : ClosestRayResultCallback
    {
        static Vector3 zero = new Vector3();

        public KinematicClosestNotMeRayResultCallback(CollisionObject me)
            : base(ref zero, ref zero)
        {
            _me = me;
        }

        public override float AddSingleResult(ref LocalRayResult rayResult, bool normalInWorldSpace)
        {
            if (rayResult.CollisionObject == _me)
                return 1.0f;

            return base.AddSingleResult(ref rayResult, normalInWorldSpace);
        }

        protected CollisionObject _me;
    }

    public class KinematicClosestNotMeConvexResultCallback : ClosestConvexResultCallback
    {
        static Vector3 zero = new Vector3();

        protected CollisionObject _me;
        protected Vector3 _up;
        protected float _minSlopeDot;

        public KinematicClosestNotMeConvexResultCallback(CollisionObject me, Vector3 up, float minSlopeDot)
            : base(ref zero, ref zero)
        {
            _me = me;
            _up = up;
            _minSlopeDot = minSlopeDot;
        }

        public override float AddSingleResult(ref LocalConvexResult convexResult, bool normalInWorldSpace)
        {
            if (convexResult.HitCollisionObject == _me)
            {
                return 1.0f;
            }

            if (!convexResult.HitCollisionObject.HasContactResponse)
            {
                return 1.0f;
            }

            Vector3 hitNormalWorld;
            if (normalInWorldSpace)
            {
                hitNormalWorld = convexResult.HitNormalLocal;
            }
            else
            {
                // need to transform normal into worldspace
                Matrix4x4 hitWorldTransform = convexResult.HitCollisionObject.WorldTransform;
                hitNormalWorld = Vector3.Transform(convexResult.HitNormalLocal, hitWorldTransform.GetBasis());
            }

            float dotUp = Vector3.Dot(_up, hitNormalWorld);
            if (dotUp < _minSlopeDot)
            {
                return 1.0f;
            }

            return base.AddSingleResult(ref convexResult, normalInWorldSpace);
        }
    }
}
