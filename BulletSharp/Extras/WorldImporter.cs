using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace BulletSharp
{
	public class WorldImporter
	{
        private DynamicsWorld _dynamicsWorld;

        protected List<OptimizedBvh> _allocatedBvhs = new List<OptimizedBvh>();
        protected List<CollisionShape> _allocatedCollisionShapes = new List<CollisionShape>();
        protected List<TypedConstraint> _allocatedConstraints = new List<TypedConstraint>();
        protected List<RigidBody> _allocatedRigidBodies = new List<RigidBody>();
        protected List<TriangleIndexVertexArray> _allocatedTriangleIndexArrays = new List<TriangleIndexVertexArray>();
        protected List<TriangleInfoMap> _allocatedTriangleInfoMaps = new List<TriangleInfoMap>();

        protected Dictionary<byte[], CollisionObject> _bodyMap = new Dictionary<byte[], CollisionObject>();
        protected Dictionary<long, OptimizedBvh> _bvhMap = new Dictionary<long, OptimizedBvh>();
        protected Dictionary<string, RigidBody> _nameBodyMap = new Dictionary<string, RigidBody>();
        protected Dictionary<string, TypedConstraint> _nameConstraintMap = new Dictionary<string, TypedConstraint>();
        protected Dictionary<string, CollisionShape> _nameShapeMap = new Dictionary<string, CollisionShape>();
        protected Dictionary<object, string> _objectNameMap = new Dictionary<object, string>();
        protected Dictionary<long, CollisionShape> _shapeMap = new Dictionary<long, CollisionShape>();
        protected FileVerboseMode _verboseMode;

		public WorldImporter(DynamicsWorld world)
		{
            _dynamicsWorld = world;
		}

        protected CollisionShape ConvertCollisionShape(byte[] data, Dictionary<long, byte[]> libPointers)
        {
            CollisionShape shape = null;

            BroadphaseNativeType type = (BroadphaseNativeType) BitConverter.ToInt32(data, CollisionShapeData.Offset("ShapeType"));
            switch (type)
            {
                case BroadphaseNativeType.StaticPlaneShape:
                {
                    Vector3 localScaling = BulletReader.ToVector3(data, StaticPlaneShapeData.Offset("LocalScaling"));
                    Vector3 planeNormal = BulletReader.ToVector3(data, StaticPlaneShapeData.Offset("PlaneNormal"));
                    float planeConstant = BitConverter.ToSingle(data, StaticPlaneShapeData.Offset("PlaneConstant"));
                    shape = CreatePlaneShape(ref planeNormal, planeConstant);
                    shape.LocalScaling = localScaling;
                    break;
                }
                case BroadphaseNativeType.GImpactShape:
                {
                    //StridingMeshInterfaceData* interfaceData = CreateStridingMeshInterfaceData(&gimpactData->m_meshInterface)
                    TriangleIndexVertexArray meshInterface = CreateMeshInterface(data,
                        GImpactMeshShapeData.Offset("MeshInterface"), libPointers);

                    GImpactShapeType gImpactType = (GImpactShapeType)BitConverter.ToSingle(data, GImpactMeshShapeData.Offset("GImpactSubType"));
                    if (gImpactType == GImpactShapeType.TrimeshShape)
                    {
                        GImpactMeshShape gimpactShape = CreateGimpactShape(meshInterface);
                        gimpactShape.LocalScaling = BulletReader.ToVector3(data, GImpactMeshShapeData.Offset("LocalScaling"));
                        gimpactShape.Margin = BitConverter.ToSingle(data, GImpactMeshShapeData.Offset("CollisionMargin"));
                        gimpactShape.UpdateBound();
                        shape = gimpactShape;
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("Unsupported GImpact subtype");
#endif
                    }
                    break;
                }
                case BroadphaseNativeType.CompoundShape:
                {
                    long childShapesPtr = BulletReader.ToPtr(data, CompoundShapeData.Offset("ChildShapePtr"));
                    int numChildShapes = BitConverter.ToInt32(data, CompoundShapeData.Offset("NumChildShapes"));
                    //int collisionMargin = BitConverter.ToInt32(data, CompoundShapeData.Offset("CollisionMargin"));
                    CompoundShape compoundShape = CreateCompoundShape();

                    byte[] childShapes = libPointers[childShapesPtr];
                    int childLength = Marshal.SizeOf(typeof(CompoundShapeChildData));
                    for (int i = 0; i < numChildShapes; i++)
                    {
                        int cs = i * childLength;
                        Matrix4x4 localTransform = BulletReader.ToMatrix(childShapes, cs + CompoundShapeChildData.Offset("Transform"));
                        long childShapePtr = BulletReader.ToPtr(childShapes, cs + CompoundShapeChildData.Offset("ChildShape"));
                        //int childShapeType = BitConverter.ToInt32(childShapes, cs + CompoundShapeChildData.Offset("ChildShapeType"));
                        //float childMargin = BitConverter.ToSingle(childShapes, cs + CompoundShapeChildData.Offset("ChildMargin"));
                        CollisionShape childShape = ConvertCollisionShape(libPointers[childShapePtr], libPointers);
                        compoundShape.AddChildShapeRef(ref localTransform, childShape);
                    }
                    shape = compoundShape;
                    break;
                }
                case BroadphaseNativeType.BoxShape:
                case BroadphaseNativeType.CapsuleShape:
                case BroadphaseNativeType.ConeShape:
                case BroadphaseNativeType.ConvexHullShape:
                case BroadphaseNativeType.CylinderShape:
                case BroadphaseNativeType.MultiSphereShape:
                case BroadphaseNativeType.SphereShape:
                {
                    Vector3 localScaling = BulletReader.ToVector3(data, ConvexInternalShapeData.Offset("LocalScaling"));
                    Vector3 implicitShapeDimensions = BulletReader.ToVector3(data, ConvexInternalShapeData.Offset("ImplicitShapeDimensions"));
                    float collisionMargin = BitConverter.ToSingle(data, ConvexInternalShapeData.Offset("CollisionMargin"));
                    switch (type)
                    {
                        case BroadphaseNativeType.BoxShape:
                        {
                            Vector3 boxExtents = implicitShapeDimensions/localScaling + new Vector3(collisionMargin);
                            BoxShape box = CreateBoxShape(ref boxExtents) as BoxShape;
                            //box.InitializePolyhedralFeatures();
                            shape = box;
                            break;
                        }
                        case BroadphaseNativeType.CapsuleShape:
                        {
                            Vector3 halfExtents = implicitShapeDimensions + new Vector3(collisionMargin);
                            int upAxis = BitConverter.ToInt32(data, CapsuleShapeData.Offset("UpAxis"));
                            switch (upAxis)
                            {
                                case 0:
                                    shape = CreateCapsuleShapeX(halfExtents.Y, halfExtents.X);
                                    break;
                                case 1:
                                    shape = CreateCapsuleShapeY(halfExtents.X, halfExtents.Y);
                                    break;
                                case 2:
                                    shape = CreateCapsuleShapeZ(halfExtents.X, halfExtents.Z);
                                    break;
                                default:
                                    Console.WriteLine("error: wrong up axis for btCapsuleShape");
                                    break;
                            }
                            break;
                        }
                        case BroadphaseNativeType.ConeShape:
                        {
                            Vector3 halfExtents = implicitShapeDimensions; // + new Vector3(collisionMargin);
                            int upAxis = BitConverter.ToInt32(data, ConeShapeData.Offset("UpAxis"));
                            switch (upAxis)
                            {
                                case 0:
                                    shape = CreateConeShapeX(halfExtents.Y, halfExtents.X);
                                    break;
                                case 1:
                                    shape = CreateConeShapeY(halfExtents.X, halfExtents.Y);
                                    break;
                                case 2:
                                    shape = CreateConeShapeZ(halfExtents.X, halfExtents.Z);
                                    break;
                                default:
                                    Console.WriteLine("unknown Cone up axis");
                                    break;
                            }
                            break;
                        }
                        case BroadphaseNativeType.ConvexHullShape:
                        {
                            long unscaledPointsFloatPtr = BulletReader.ToPtr(data, ConvexHullShapeData.Offset("UnscaledPointsFloatPtr"));
                            long unscaledPointsDoublePtr = BulletReader.ToPtr(data, ConvexHullShapeData.Offset("UnscaledPointsDoublePtr"));
                            int numPoints = BitConverter.ToInt32(data, ConvexHullShapeData.Offset("NumUnscaledPoints"));
                            bool isFloat = unscaledPointsFloatPtr != 0;

                            byte[] points = libPointers[isFloat ? unscaledPointsFloatPtr : unscaledPointsDoublePtr];
                            ConvexHullShape hullShape = CreateConvexHullShape();
                            int vectorLength = 4 * (isFloat ? sizeof(float) : sizeof(double));
                            for (int i = 0; i < numPoints; i++)
                            {
                                int v = i * vectorLength;
                                Vector3 point = isFloat
                                    ? BulletReader.ToVector3(points, v)
                                    : BulletReader.ToVector3Double(points, v);
                                hullShape.AddPoint(point);
                            }
                            hullShape.Margin = collisionMargin;
                            //hullShape.InitializePolyhedralFeatures();
                            shape = hullShape;
                            break;
                        }
                        case BroadphaseNativeType.CylinderShape:
                        {
                            Vector3 halfExtents = implicitShapeDimensions + new Vector3(collisionMargin);
                            int upAxis = BitConverter.ToInt32(data, CylinderShapeData.Offset("UpAxis"));
                            switch (upAxis)
                            {
                                case 0:
                                    shape = CreateCylinderShapeX(halfExtents.Y, halfExtents.X);
                                    break;
                                case 1:
                                    shape = CreateCylinderShapeY(halfExtents.X, halfExtents.Y);
                                    break;
                                case 2:
                                    shape = CreateCylinderShapeZ(halfExtents.X, halfExtents.Z);
                                    break;
                                default:
                                    Console.WriteLine("unknown Cylinder up axis");
                                    break;
                            }
                            break;
                        }
                        case BroadphaseNativeType.MultiSphereShape:
                        {
                            long localPositionArrayPtr = BulletReader.ToPtr(data, MultiSphereShapeData.Offset("LocalPositionArrayPtr"));
                            int localPositionArraySize = BitConverter.ToInt32(data, MultiSphereShapeData.Offset("LocalPositionArraySize"));
                            byte[] localPositionArray = libPointers[localPositionArrayPtr];
                            Vector3[] positions = new Vector3[localPositionArraySize];
                            float[] radi = new float[localPositionArraySize];
                            int positionAndRadiusLength = Marshal.SizeOf(typeof(PositionAndRadius));
                            for (int i = 0; i < localPositionArraySize; i++)
                            {
                                int p = i * positionAndRadiusLength;
                                positions[i] = BulletReader.ToVector3(localPositionArray, p + PositionAndRadius.Offset("Position"));
                                radi[i] = BitConverter.ToSingle(localPositionArray, p + PositionAndRadius.Offset("Radius"));
                            }
                            shape = CreateMultiSphereShape(positions, radi);
                            break;
                        }
                        case BroadphaseNativeType.SphereShape:
                        {
                            shape = CreateSphereShape(implicitShapeDimensions.X);
                            break;
                        }
                    }
                    if (shape != null)
                    {
                        shape.LocalScaling = localScaling;
                    }
                    break;
                }
                case BroadphaseNativeType.TriangleMeshShape:
                {
                    TriangleIndexVertexArray meshInterface = CreateMeshInterface(data,
                        TriangleMeshShapeData.Offset("MeshInterface"), libPointers);
                    if (meshInterface.NumSubParts == 0)
                    {
                        return null;
                    }
                    OptimizedBvh bvh = null;
                    long bvhPtr = BulletReader.ToPtr(data, TriangleMeshShapeData.Offset("QuantizedFloatBvh"));
                    if (bvhPtr != 0)
                    {
                        if (_bvhMap.ContainsKey(bvhPtr))
                        {
                            bvh = _bvhMap[bvhPtr];
                        }
                        else
                        {
                            bvh = CreateOptimizedBvh();
                            throw new NotImplementedException();
                            //bvh.DeserializeFloat(bvhPtr);
                        }
                    }
                    bvhPtr = BulletReader.ToPtr(data, TriangleMeshShapeData.Offset("QuantizedDoubleBvh"));
                    if (bvhPtr != 0)
                    {
                        throw new NotImplementedException();
                    }
                    BvhTriangleMeshShape trimeshShape = CreateBvhTriangleMeshShape(meshInterface, bvh);
                    trimeshShape.Margin = BitConverter.ToSingle(data, TriangleMeshShapeData.Offset("CollisionMargin"));
                    shape = trimeshShape;
                    break;
                }
                case BroadphaseNativeType.SoftBodyShape:
                    return null;
                default:
#if DEBUG
                    Console.WriteLine("Unsupported shape type ({0})\n", type);
#endif
                    throw new NotImplementedException();
            }

            return shape;
        }

        protected void ConvertConstraintFloat(RigidBody rigidBodyA, RigidBody rigidBodyB, byte[] data, int fileVersion, Dictionary<long, byte[]> libPointers)
        {
            TypedConstraint constraint = null;

            TypedConstraintType type = (TypedConstraintType)BitConverter.ToInt32(data, TypedConstraintFloatData.Offset("ObjectType"));
            switch (type)
            {
                case TypedConstraintType.Point2Point:
                {
                    Vector3 pivotInA = BulletReader.ToVector3(data, Point2PointConstraintFloatData.Offset("PivotInA"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Vector3 pivotInB = BulletReader.ToVector3(data, Point2PointConstraintFloatData.Offset("PivotInB"));
                        constraint = CreatePoint2PointConstraint(rigidBodyA, rigidBodyB, ref pivotInA, ref pivotInB);
                    }
                    else
                    {
                        constraint = CreatePoint2PointConstraint(rigidBodyA, ref pivotInA);
                    }
                    break;
                }
                case TypedConstraintType.ConeTwist:
                {
                    ConeTwistConstraint coneTwist;
                    Matrix4x4 rbaFrame = BulletReader.ToMatrix(data, ConeTwistConstraintFloatData.Offset("RigidBodyAFrame"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, ConeTwistConstraintFloatData.Offset("RigidBodyBFrame"));
                        coneTwist = CreateConeTwistConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame);
                    }
                    else
                    {
                        coneTwist = CreateConeTwistConstraint(rigidBodyA, ref rbaFrame);
                    }
                    coneTwist.SetLimit(
                        BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("SwingSpan1")),
                        BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("SwingSpan2")),
                        BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("TwistSpan")),
                        BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("LimitSoftness")),
                        BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("BiasFactor")),
                        BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("RelaxationFactor")));
                    coneTwist.Damping = BitConverter.ToSingle(data, ConeTwistConstraintFloatData.Offset("Damping"));

                    constraint = coneTwist;
                    break;
                }
                case TypedConstraintType.D6:
                {
                    Generic6DofConstraint dof = null;
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix4x4 rbaFrame = BulletReader.ToMatrix(data, Generic6DofConstraintFloatData.Offset("RigidBodyAFrame"));
                        Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                        int useLinearReferenceFrameA =
                            BitConverter.ToInt32(data, Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                        dof = CreateGeneric6DofConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useLinearReferenceFrameA != 0);
                    }
                    else
                    {
                        if (rigidBodyB != null)
                        {
                            Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                            int useLinearReferenceFrameA =
                                BitConverter.ToInt32(data, Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                            dof = CreateGeneric6DofConstraint(rigidBodyB, ref rbbFrame, useLinearReferenceFrameA != 0);
                        }
                        else
                        {
                            Console.WriteLine("Error in WorldImporter.CreateGeneric6DofConstraint: missing rigidBodyB");
                        }
                    }

                    if (dof != null)
                    {
                        dof.AngularLowerLimit =
                            BulletReader.ToVector3(data, Generic6DofConstraintFloatData.Offset("AngularLowerLimit"));
                        dof.AngularUpperLimit =
                            BulletReader.ToVector3(data, Generic6DofConstraintFloatData.Offset("AngularUpperLimit"));
                        dof.LinearLowerLimit =
                            BulletReader.ToVector3(data, Generic6DofConstraintFloatData.Offset("LinearLowerLimit"));
                        dof.LinearUpperLimit =
                            BulletReader.ToVector3(data, Generic6DofConstraintFloatData.Offset("LinearUpperLimit"));
                    }
                    constraint = dof;
                    break;
                }
                case TypedConstraintType.D6Spring:
                {
                    Generic6DofSpringConstraint dof = null;
                    int sixDofData = Generic6DofSpringConstraintFloatData.Offset("SixDofData");
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix4x4 rbaFrame = BulletReader.ToMatrix(data, sixDofData + Generic6DofConstraintFloatData.Offset("RigidBodyAFrame"));
                        Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, sixDofData + Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                        int useLinearReferenceFrameA = BitConverter.ToInt32(data, sixDofData +
                            Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                        dof = CreateGeneric6DofSpringConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useLinearReferenceFrameA != 0);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error in WorldImporter.CreateGeneric6DofSpringConstraint: requires rigidBodyA && rigidBodyB");
                    }

                    if (dof != null)
                    {
                        dof.AngularLowerLimit = BulletReader.ToVector3(data, sixDofData + Generic6DofConstraintFloatData.Offset("AngularLowerLimit"));
                        dof.AngularUpperLimit = BulletReader.ToVector3(data, sixDofData + Generic6DofConstraintFloatData.Offset("AngularUpperLimit"));
                        dof.LinearLowerLimit = BulletReader.ToVector3(data, sixDofData + Generic6DofConstraintFloatData.Offset("LinearLowerLimit"));
                        dof.LinearUpperLimit = BulletReader.ToVector3(data, sixDofData + Generic6DofConstraintFloatData.Offset("LinearUpperLimit"));

                        int i;
                        if (fileVersion > 280)
                        {
                            int springEnabledOffset = Generic6DofSpringConstraintFloatData.Offset("SpringEnabled");
                            int equilibriumPointOffset = Generic6DofSpringConstraintFloatData.Offset("EquilibriumPoint");
                            int springStiffnessOffset = Generic6DofSpringConstraintFloatData.Offset("SpringStiffness");
                            int springDampingOffset = Generic6DofSpringConstraintFloatData.Offset("SpringDamping");
                            for (i = 0; i < 6; i++)
                            {
                                dof.SetStiffness(i, BitConverter.ToSingle(data, springStiffnessOffset + sizeof (float)*i));
                                dof.SetEquilibriumPoint(i, BitConverter.ToSingle(data, equilibriumPointOffset + sizeof (float)*i));
                                dof.EnableSpring(i, BitConverter.ToInt32(data, springEnabledOffset + sizeof (int)*i) != 0);
                                dof.SetDamping(i, BitConverter.ToSingle(data, springDampingOffset + sizeof (float)*i));
                            }
                        }
                    }
                    constraint = dof;
                    break;
                }
                case TypedConstraintType.D6Spring2:
                {
                    Generic6DofSpring2Constraint dof = null;
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix4x4 rbaFrame = BulletReader.ToMatrix(data, Generic6DofSpring2ConstraintFloatData.Offset("RigidBodyAFrame"));
                        Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, Generic6DofSpring2ConstraintFloatData.Offset("RigidBodyBFrame"));
                        RotateOrder rotateOrder = (RotateOrder)BitConverter.ToInt32(data, Generic6DofSpring2ConstraintFloatData.Offset("RotateOrder"));
                        dof = CreateGeneric6DofSpring2Constraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame, rotateOrder);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error in WorldImporter.CreateGeneric6DofSpring2Constraint: requires rigidBodyA && rigidBodyB");
                    }

                    if (dof != null)
                    {
                        dof.AngularLowerLimit = BulletReader.ToVector3(data, Generic6DofSpring2ConstraintFloatData.Offset("AngularLowerLimit"));
                        dof.AngularUpperLimit = BulletReader.ToVector3(data, Generic6DofSpring2ConstraintFloatData.Offset("AngularUpperLimit"));
                        dof.LinearLowerLimit = BulletReader.ToVector3(data, Generic6DofSpring2ConstraintFloatData.Offset("LinearLowerLimit"));
                        dof.LinearUpperLimit = BulletReader.ToVector3(data, Generic6DofSpring2ConstraintFloatData.Offset("LinearUpperLimit"));

                        int i;
                        if (fileVersion > 280)
                        {
                            int linearSpringStiffnessOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringStiffness");
                            int linearSpringStiffnessLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringStiffnessLimited");
                            int linearEnableSpringdOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearEnableSpring");
                            int linearEquilibriumPointOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearEquilibriumPoint");
                            int linearSpringDampingOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringDamping");
                            int linearSpringDampingLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringDampingLimited");
                            for (i = 0; i < 3; i++)
                            {
                                dof.SetStiffness(i, BitConverter.ToSingle(data, linearSpringStiffnessOffset + sizeof (float)*i),
                                    data[linearSpringStiffnessLimitedOffset + sizeof (byte)*i] != 0);
                                dof.SetEquilibriumPoint(i, BitConverter.ToSingle(data, linearEquilibriumPointOffset + sizeof (float)*i));
                                dof.EnableSpring(i, BitConverter.ToInt32(data, linearEnableSpringdOffset + sizeof (byte)*i) != 0);
                                dof.SetDamping(i, BitConverter.ToSingle(data, linearSpringDampingOffset + sizeof (float)*i),
                                    data[linearSpringDampingLimitedOffset + sizeof (float)*i] != 0);
                            }

                            int angularSpringStiffnessOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringStiffness");
                            int angularSpringStiffnessLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringStiffnessLimited");
                            int angularEnableSpringdOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularEnableSpring");
                            int angularEquilibriumPointOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularEquilibriumPoint");
                            int angularSpringDampingOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringDamping");
                            int angularSpringDampingLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringDampingLimited");
                            for (i = 0; i < 3; i++)
                            {
                                dof.SetStiffness(i + 3,
                                    BitConverter.ToSingle(data, angularSpringStiffnessOffset + sizeof (float)*i),
                                    data[angularSpringStiffnessLimitedOffset + sizeof (byte)*i] != 0);
                                dof.SetEquilibriumPoint(i + 3,
                                    BitConverter.ToSingle(data, angularEquilibriumPointOffset + sizeof (float)*i));
                                dof.EnableSpring(i + 3,
                                    BitConverter.ToInt32(data, angularEnableSpringdOffset + sizeof (byte)*i) != 0);
                                dof.SetDamping(i + 3, BitConverter.ToSingle(data, angularSpringDampingOffset + sizeof (float)*i),
                                    data[angularSpringDampingLimitedOffset + sizeof (float)*i] != 0);
                            }
                        }
                    }
                    constraint = dof;
                    break;
                }
                case TypedConstraintType.Gear:
                {
                    GearConstraint gear;
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Vector3 axisInA = BulletReader.ToVector3(data, GearConstraintFloatData.Offset("AxisInA"));
                        Vector3 axisInB = BulletReader.ToVector3(data, GearConstraintFloatData.Offset("AxisInB"));
                        float ratio = BitConverter.ToSingle(data, GearConstraintFloatData.Offset("Ratio"));
                        gear = CreateGearConstraint(rigidBodyA, rigidBodyB, ref axisInA, ref axisInB, ratio);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    constraint = gear;
                    break;
                }
                case TypedConstraintType.Hinge:
                {
                    HingeConstraint hinge;
                    Matrix4x4 rbaFrame = BulletReader.ToMatrix(data, HingeConstraintFloatData.Offset("RigidBodyAFrame"));
                    int useReferenceFrameA = BitConverter.ToInt32(data, HingeConstraintFloatData.Offset("UseReferenceFrameA"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, HingeConstraintFloatData.Offset("RigidBodyBFrame"));
                        hinge = CreateHingeConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useReferenceFrameA != 0);
                    }
                    else
                    {
                        hinge = CreateHingeConstraint(rigidBodyA, ref rbaFrame, useReferenceFrameA != 0);
                    }
                    if (BitConverter.ToInt32(data, HingeConstraintFloatData.Offset("EnableAngularMotor")) != 0)
                    {
                        hinge.EnableAngularMotor(true,
                            BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("MotorTargetVelocity")),
                            BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("MaxMotorImpulse")));
                    }
                    hinge.AngularOnly = BitConverter.ToInt32(data, HingeConstraintFloatData.Offset("AngularOnly")) != 0;
                    hinge.SetLimit(
                        BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("LowerLimit")),
                        BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("UpperLimit")),
                        BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("LimitSoftness")),
                        BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("BiasFactor")),
                        BitConverter.ToSingle(data, HingeConstraintFloatData.Offset("RelaxationFactor")));
                    constraint = hinge;
                    break;
                }
                case TypedConstraintType.Slider:
                {
                    SliderConstraint slider;
                    Matrix4x4 rbbFrame = BulletReader.ToMatrix(data, SliderConstraintFloatData.Offset("RigidBodyBFrame"));
                    int useLinearReferenceFrameA =
                        BitConverter.ToInt32(data, SliderConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix4x4 rbaFrame = BulletReader.ToMatrix(data, SliderConstraintFloatData.Offset("RigidBodyAFrame"));
                        slider = CreateSliderConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useLinearReferenceFrameA != 0);
                    }
                    else
                    {
                        slider = CreateSliderConstraint(rigidBodyB, ref rbbFrame, useLinearReferenceFrameA != 0);
                    }
                    slider.LowerLinearLimit = BitConverter.ToSingle(data, SliderConstraintFloatData.Offset("LinearLowerLimit"));
                    slider.UpperLinearLimit = BitConverter.ToSingle(data, SliderConstraintFloatData.Offset("LinearUpperLimit"));
                    slider.LowerAngularLimit = BitConverter.ToSingle(data, SliderConstraintFloatData.Offset("AngularLowerLimit"));
                    slider.UpperAngularLimit = BitConverter.ToSingle(data, SliderConstraintFloatData.Offset("AngularUpperLimit"));
                    slider.UseFrameOffset =
                        BitConverter.ToInt32(data, SliderConstraintFloatData.Offset("UseOffsetForConstraintFrame")) != 0;
                    constraint = slider;
                    break;
                }
                case TypedConstraintType.Fixed:
                {
                    if (rigidBodyA == null || rigidBodyB == null)
                    {
                        throw new InvalidDataException("Error: requires rigidBodyA && rigidBodyB");
                    }

                    Matrix4x4 rbaFrame = rigidBodyA.WorldTransform;
                    Matrix4x4 rbbFrame = rigidBodyB.WorldTransform;
                    Matrix4x4 sharedFrame = Matrix4x4.CreateTranslation(0.5f * (rbaFrame.Translation + rbbFrame.Translation));
                    Matrix4x4.Invert(rbaFrame, out rbaFrame);
                    Matrix4x4.Invert(rbbFrame, out rbbFrame);
                    rbaFrame = rbaFrame * sharedFrame;
                    rbbFrame = rbbFrame * sharedFrame;
                    Generic6DofSpring2Constraint dof = new Generic6DofSpring2Constraint(rigidBodyA, rigidBodyB, rbaFrame, rbbFrame, RotateOrder.XYZ)
                    {
                        LinearLowerLimit = Vector3.Zero,
                        LinearUpperLimit = Vector3.Zero,
                        AngularLowerLimit = Vector3.Zero,
                        AngularUpperLimit = Vector3.Zero
                    };
                    constraint = dof;
                    break;
                }
                default:
                    throw new NotImplementedException();
            }

            if (constraint != null)
            {
                constraint.DebugDrawSize = BitConverter.ToSingle(data, TypedConstraintFloatData.Offset("DebugDrawSize"));
                // those fields didn't exist and set to zero for pre-280 versions, so do a check here
                if (fileVersion >= 280)
                {
                    constraint.BreakingImpulseThreshold = BitConverter.ToSingle(data, TypedConstraintFloatData.Offset("BreakingImpulseThreshold"));
                    constraint.IsEnabled = BitConverter.ToInt32(data, TypedConstraintFloatData.Offset("IsEnabled")) != 0;
                    constraint.OverrideNumSolverIterations = BitConverter.ToInt32(data, TypedConstraintFloatData.Offset("OverrideNumSolverIterations"));
                }

                long namePtr = BulletReader.ToPtr(data, TypedConstraintFloatData.Offset("Name"));
                if (namePtr != 0)
                {
                    byte[] nameData = libPointers[namePtr];
                    int length = Array.IndexOf(nameData, (byte)0);
                    string name = Encoding.ASCII.GetString(nameData, 0, length);
                    _nameConstraintMap.Add(name, constraint);
                    _objectNameMap.Add(constraint, name);
                }

                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.AddConstraint(constraint);
                }
            }
        }

        protected void ConvertRigidBodyFloat(byte[] data, Dictionary<long, byte[]> libPointers)
        {
            int cod = RigidBodyFloatData.Offset("CollisionObjectData");
            long collisionShapePtr = BulletReader.ToPtr(data, cod + CollisionObjectFloatData.Offset("CollisionShape"));
            Matrix4x4 startTransform = BulletReader.ToMatrix(data, cod + CollisionObjectFloatData.Offset("WorldTransform"));
            long namePtr = BulletReader.ToPtr(data, cod + CollisionObjectFloatData.Offset("Name"));
            float friction = BitConverter.ToSingle(data, cod + CollisionObjectFloatData.Offset("Friction"));
            float restitution = BitConverter.ToSingle(data, cod + CollisionObjectFloatData.Offset("Restitution"));

            float inverseMass = BitConverter.ToSingle(data, RigidBodyFloatData.Offset("InverseMass"));
            Vector3 angularFactor = BulletReader.ToVector3(data, RigidBodyFloatData.Offset("AngularFactor"));
            Vector3 linearFactor = BulletReader.ToVector3(data, RigidBodyFloatData.Offset("LinearFactor"));

            CollisionShape shape = _shapeMap[collisionShapePtr];

            float mass;
            bool isDynamic;
            if (shape.IsNonMoving)
            {
                mass = 0.0f;
                isDynamic = false;
            }
            else
            {
                isDynamic = inverseMass != 0;
                mass = isDynamic ? 1.0f / inverseMass : 0;
            }
            string name = null;
            if (namePtr != 0)
            {
                byte[] nameData = libPointers[namePtr];
                int length = Array.IndexOf(nameData, (byte)0);
                name = Encoding.ASCII.GetString(nameData, 0, length);
            }

            RigidBody body = CreateRigidBody(isDynamic, mass, ref startTransform, shape, name);
            body.Friction = friction;
            body.Restitution = restitution;
            body.AngularFactor = angularFactor;
            body.LinearFactor = linearFactor;
            _bodyMap.Add(data, body);
        }

        protected void ConvertRigidBodyDouble(byte[] data, Dictionary<long, byte[]> libPointers)
        {
            int cod = RigidBodyFloatData.Offset("CollisionObjectData");
            long collisionShapePtr = BulletReader.ToPtr(data, cod + CollisionObjectFloatData.Offset("CollisionShape"));
            Matrix4x4 startTransform = BulletReader.ToMatrixDouble(data, cod + CollisionObjectFloatData.Offset("WorldTransform"));
            long namePtr = BulletReader.ToPtr(data, cod + CollisionObjectFloatData.Offset("Name"));
            double friction = BitConverter.ToDouble(data, cod + CollisionObjectFloatData.Offset("Friction"));
            double restitution = BitConverter.ToDouble(data, cod + CollisionObjectFloatData.Offset("Restitution"));

            double inverseMass = BitConverter.ToDouble(data, RigidBodyFloatData.Offset("InverseMass"));
            Vector3 angularFactor = BulletReader.ToVector3Double(data, RigidBodyFloatData.Offset("AngularFactor"));
            Vector3 linearFactor = BulletReader.ToVector3Double(data, RigidBodyFloatData.Offset("LinearFactor"));

            CollisionShape shape = _shapeMap[collisionShapePtr];

            float mass;
            bool isDynamic;
            if (shape.IsNonMoving)
            {
                mass = 0.0f;
                isDynamic = false;
            }
            else
            {
                isDynamic = inverseMass != 0;
                mass = isDynamic ? 1.0f / (float)inverseMass : 0;
            }
            string name = null;
            if (namePtr != 0)
            {
                byte[] nameData = libPointers[namePtr];
                int length = Array.IndexOf(nameData, (byte)0);
                name = Encoding.ASCII.GetString(nameData, 0, length);
            }

            RigidBody body = CreateRigidBody(isDynamic, mass, ref startTransform, shape, name);
            body.Friction = (float)friction;
            body.Restitution = (float)restitution;
            body.AngularFactor = angularFactor;
            body.LinearFactor = linearFactor;
            _bodyMap.Add(data, body);
        }

        public CollisionShape CreateBoxShape(ref Vector3 halfExtents)
		{
            BoxShape shape = new BoxShape(halfExtents);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public BvhTriangleMeshShape CreateBvhTriangleMeshShape(StridingMeshInterface trimesh, OptimizedBvh bvh)
		{
            BvhTriangleMeshShape shape;
            if (bvh != null)
            {
                shape = new BvhTriangleMeshShape(trimesh, bvh.IsQuantized, false);
                shape.OptimizedBvh = bvh;
            }
            else
            {
                shape = new BvhTriangleMeshShape(trimesh, true);
            }
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeZ(float radius, float height)
		{
            CapsuleShapeZ shape = new CapsuleShapeZ(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeX(float radius, float height)
		{
            CapsuleShapeX shape = new CapsuleShapeX(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeY(float radius, float height)
		{
            CapsuleShape shape = new CapsuleShape(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionObject CreateCollisionObject(ref Matrix4x4 startTransform, CollisionShape shape, string bodyName)
		{
            return CreateRigidBody(false, 0, ref startTransform, shape, bodyName);
		}

		public CompoundShape CreateCompoundShape()
		{
            CompoundShape shape = new CompoundShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeZ(float radius, float height)
		{
			ConeShape shape = new ConeShapeZ(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeX(float radius, float height)
		{
			ConeShape shape = new ConeShapeX(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeY(float radius, float height)
		{
			ConeShape shape = new ConeShape(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, ref Matrix4x4 rbAFrame)
		{
            ConeTwistConstraint constraint = new ConeTwistConstraint(rbA, rbAFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 rbAFrame, ref Matrix4x4 rbBFrame)
		{
            ConeTwistConstraint constraint = new ConeTwistConstraint(rbA, rbB, rbAFrame, rbBFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public ConvexHullShape CreateConvexHullShape()
		{
            ConvexHullShape shape = new ConvexHullShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        /*
		public CollisionShape CreateConvexTriangleMeshShape(StridingMeshInterface trimesh)
		{
			return btWorldImporter_createConvexTriangleMeshShape(_native, trimesh._native);
		}
        */
		public CollisionShape CreateCylinderShapeZ(float radius, float height)
		{
            CylinderShapeZ shape = new CylinderShapeZ(radius, radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCylinderShapeX(float radius, float height)
		{
            CylinderShapeX shape = new CylinderShapeX(height, radius, radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCylinderShapeY(float radius, float height)
		{
            CylinderShape shape = new CylinderShape(radius, height, radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public GearConstraint CreateGearConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 axisInA, ref Vector3 axisInB, float ratio)
		{
			GearConstraint constraint = new GearConstraint(rbA, rbB, axisInA, axisInB, ratio);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbB, ref Matrix4x4 frameInB, bool useLinearReferenceFrameB)
		{
			Generic6DofConstraint constraint = new Generic6DofConstraint(rbB, frameInB, useLinearReferenceFrameB);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 frameInA, ref Matrix4x4 frameInB, bool useLinearReferenceFrameA)
		{
			Generic6DofConstraint constraint = new Generic6DofConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofSpringConstraint CreateGeneric6DofSpringConstraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 frameInA, ref Matrix4x4 frameInB, bool useLinearReferenceFrameA)
		{
			Generic6DofSpringConstraint constraint = new Generic6DofSpringConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public Generic6DofSpring2Constraint CreateGeneric6DofSpring2Constraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 frameInA, ref Matrix4x4 frameInB, RotateOrder rotateOrder)
        {
            Generic6DofSpring2Constraint constraint = new Generic6DofSpring2Constraint(rbA, rbB, frameInA, frameInB, rotateOrder);
            _allocatedConstraints.Add(constraint);
            return constraint;
        }

		public GImpactMeshShape CreateGimpactShape(StridingMeshInterface trimesh)
		{
            var shape = new GImpactMeshShape(trimesh);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 rbAFrame, ref Matrix4x4 rbBFrame, bool useReferenceFrameA)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbB, rbAFrame, rbBFrame, useReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 rbAFrame, ref Matrix4x4 rbBFrame)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbB, rbAFrame, rbBFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, ref Matrix4x4 rbAFrame, bool useReferenceFrameA)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbAFrame, useReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, ref Matrix4x4 rbAFrame)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbAFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public TriangleIndexVertexArray CreateMeshInterface(byte[] data, int offset, Dictionary<long, byte[]> libPointers)
        {
            TriangleIndexVertexArray meshInterface = CreateTriangleMeshContainer();
            long meshPartsPtr = BulletReader.ToPtr(data, offset + StridingMeshInterfaceData.Offset("MeshPartsPtr"));
            Vector3 scaling = BulletReader.ToVector3(data, offset + StridingMeshInterfaceData.Offset("Scaling"));
            int numMeshParts = BitConverter.ToInt32(data, offset + StridingMeshInterfaceData.Offset("NumMeshParts"));

            byte[] meshParts = libPointers[meshPartsPtr];
                    int meshPartDataLength = Marshal.SizeOf(typeof(MeshPartData));
            for (int i = 0; i < numMeshParts; i++)
            {
                int meshOffset = i * meshPartDataLength;

                IndexedMesh meshPart = new IndexedMesh();
                long vertices3f = BulletReader.ToPtr(meshParts, meshOffset + MeshPartData.Offset("Vertices3F"));
                long vertices3d = BulletReader.ToPtr(meshParts, meshOffset + MeshPartData.Offset("Vertices3D"));
                long indices32 = BulletReader.ToPtr(meshParts, meshOffset + MeshPartData.Offset("Indices32"));
                meshPart.NumTriangles = BitConverter.ToInt32(meshParts, meshOffset + MeshPartData.Offset("NumTriangles"));
                meshPart.NumVertices = BitConverter.ToInt32(meshParts, meshOffset + MeshPartData.Offset("NumVertices"));
                meshPart.Allocate(meshPart.NumTriangles, meshPart.NumVertices, sizeof(int) * 3, sizeof(float) * 4);

                if (indices32 != 0)
                {
                    using (Stream triangleStream = meshPart.GetTriangleStream())
                    {
                        byte[] indices = libPointers[indices32];
                        triangleStream.Write(indices, 0, indices.Length);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                    //long indices16 = meshReader.ReadPtr(meshOffset + MeshPartData.Offset("Indices16"));
                }

                if (vertices3f != 0)
                {
                    using (Stream vertexStream = meshPart.GetVertexStream())
                    {
                        byte[] vertices = libPointers[vertices3f];
                        vertexStream.Write(vertices, 0, vertices.Length);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                if (meshPart.TriangleIndexBase != IntPtr.Zero && meshPart.VertexBase != IntPtr.Zero)
                {
                    meshInterface.AddIndexedMesh(meshPart, meshPart.IndexType);
                }
            }
            return meshInterface;
        }

        public MultiSphereShape CreateMultiSphereShape(Vector3[] positions, float[] radi)
		{
			MultiSphereShape shape = new MultiSphereShape(positions, radi);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        
		public OptimizedBvh CreateOptimizedBvh()
		{
			OptimizedBvh bvh = new OptimizedBvh();
            _allocatedBvhs.Add(bvh);
            return bvh;
		}
        
		public CollisionShape CreatePlaneShape(ref Vector3 planeNormal, float planeConstant)
		{
            StaticPlaneShape shape = new StaticPlaneShape(planeNormal, planeConstant);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 pivotInA, ref Vector3 pivotInB)
		{
            Point2PointConstraint constraint = new Point2PointConstraint(rbA, rbB, pivotInA, pivotInB);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, ref Vector3 pivotInA)
		{
			Point2PointConstraint constraint = new Point2PointConstraint(rbA, pivotInA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public virtual RigidBody CreateRigidBody(bool isDynamic, float mass, ref Matrix4x4 startTransform, CollisionShape shape, string bodyName)
		{
            Vector3 localInertia;
            if (mass != 0.0f)
            {
                shape.CalculateLocalInertia(mass, out localInertia);
            }
            else
            {
                localInertia = Vector3.Zero;
            }

            RigidBody body;
            using (var info = new RigidBodyConstructionInfo(mass, null, shape, localInertia))
            {
                body = new RigidBody(info);
            }
            body.WorldTransform = startTransform;

            if (_dynamicsWorld != null)
            {
                _dynamicsWorld.AddRigidBody(body);
            }

            if (bodyName != null)
            {
                _objectNameMap.Add(body, bodyName);
                _nameBodyMap[bodyName] = body;
            }
            _allocatedRigidBodies.Add(body);
            return body;
		}

		public ScaledBvhTriangleMeshShape CreateScaledTriangleMeshShape(BvhTriangleMeshShape meshShape, ref Vector3 localScalingbtBvhTriangleMeshShape)
		{
            ScaledBvhTriangleMeshShape shape = new ScaledBvhTriangleMeshShape(meshShape, localScalingbtBvhTriangleMeshShape);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbB, ref Matrix4x4 frameInB, bool useLinearReferenceFrameA)
		{
			SliderConstraint constraint = new SliderConstraint(rbB, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbA, RigidBody rbB, ref Matrix4x4 frameInA, ref Matrix4x4 frameInB, bool useLinearReferenceFrameA)
		{
            SliderConstraint constraint = new SliderConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public CollisionShape CreateSphereShape(float radius)
		{
			SphereShape shape = new SphereShape(radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public byte[] CreateStridingMeshInterfaceData(byte[] interfaceData, int offset = 0)
		{
            return null;
		}

		public TriangleInfoMap CreateTriangleInfoMap()
		{
            TriangleInfoMap tim = new TriangleInfoMap();
            _allocatedTriangleInfoMaps.Add(tim);
            return tim;
		}

        public TriangleIndexVertexArray CreateTriangleMeshContainer()
		{
            TriangleIndexVertexArray tiva = new TriangleIndexVertexArray();
            _allocatedTriangleIndexArrays.Add(tiva);
            return tiva;
		}

		public void DeleteAllData()
		{
            foreach (TypedConstraint constraint in _allocatedConstraints)
            {
                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.RemoveConstraint(constraint);
                }
                constraint.Dispose();
            }
            _allocatedConstraints.Clear();

            foreach (RigidBody rigidBody in _allocatedRigidBodies)
            {
                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.RemoveRigidBody(rigidBody);
                }
                rigidBody.Dispose();
            }
            _allocatedRigidBodies.Clear();

            foreach (CollisionShape shape in _allocatedCollisionShapes)
            {
                shape.Dispose();
            }
            _allocatedCollisionShapes.Clear();

            foreach (OptimizedBvh bvh in _allocatedBvhs)
            {
                bvh.Dispose();
            }
            _allocatedBvhs.Clear();
        
            foreach (TriangleInfoMap tim in _allocatedTriangleInfoMaps)
            {
                tim.Dispose();
            }
            _allocatedTriangleInfoMaps.Clear();

            foreach (TriangleIndexVertexArray tiva in _allocatedTriangleIndexArrays)
            {
                tiva.Dispose();
            }
            _allocatedTriangleIndexArrays.Clear();

            //TODO: _allocatedbtStridingMeshInterfaceDatas
		}
        
		public OptimizedBvh GetBvhByIndex(int index)
		{
            return _allocatedBvhs[index];
		}
        
		public CollisionShape GetCollisionShapeByIndex(int index)
		{
            return _allocatedCollisionShapes[index];
		}

		public CollisionShape GetCollisionShapeByName(string name)
		{
            CollisionShape shape;
            _nameShapeMap.TryGetValue(name, out shape);
            return shape;
		}

		public TypedConstraint GetConstraintByIndex(int index)
		{
            return _allocatedConstraints[index];
		}

		public TypedConstraint GetConstraintByName(string name)
		{
            TypedConstraint constraint;
            _nameConstraintMap.TryGetValue(name, out constraint);
            return constraint;
		}

		public string GetNameForObject(object obj)
		{
            string name;
            _objectNameMap.TryGetValue(obj, out name);
            return name;
		}

		public CollisionObject GetRigidBodyByIndex(int index)
		{
            return _allocatedRigidBodies[index];
		}

		public RigidBody GetRigidBodyByName(string name)
		{
            RigidBody body;
            _nameBodyMap.TryGetValue(name, out body);
            return body;
		}
        
		public TriangleInfoMap GetTriangleInfoMapByIndex(int index)
		{
            return _allocatedTriangleInfoMaps[index];
		}
        
		public void SetDynamicsWorldInfo(ref Vector3 gravity, ContactSolverInfo solverInfo)
		{
            if (_dynamicsWorld != null)
            {
                _dynamicsWorld.SetGravity(ref gravity);
            }
		}

		public int NumBvhs
		{
            get { return _allocatedBvhs.Count; }
		}

		public int NumCollisionShapes
		{
            get { return _allocatedCollisionShapes.Count; }
		}

		public int NumConstraints
		{
			get { return _allocatedConstraints.Count; }
		}

		public int NumRigidBodies
		{
            get { return _allocatedRigidBodies.Count; }
		}

		public int NumTriangleInfoMaps
		{
            get { return _allocatedTriangleInfoMaps.Count; }
		}

        public FileVerboseMode VerboseMode
        {
            get { return _verboseMode; }
            set { _verboseMode = value; }
        }
	}
}
