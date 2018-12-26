using BulletSharp.Math;

namespace BulletSharp
{
    public interface ICharacterController : IAction
    {
        void SetWalkDirection(ref Vector3 walkDirection);
        void SetVelocityForTimeInterval(ref Vector3 velocity, double timeInterval);
        void Reset(CollisionWorld collisionWorld);
        void Warp(ref Vector3 origin);

        void PreStep(CollisionWorld collisionWorld);
        void PlayerStep(CollisionWorld collisionWorld, double deltaTime);
        void Jump();
        void Jump(ref Vector3 dir);

        void SetUpInterpolate(bool value);

        bool OnGround { get; }
        bool CanJump { get; }
    }
}
