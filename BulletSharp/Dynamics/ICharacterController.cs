using BulletSharp.Math;

namespace BulletSharp
{
	public interface ICharacterController : IAction
	{
        void SetWalkDirection(ref Vector3 walkDirection);
        void SetVelocityForTimeInterval(ref Vector3 velocity, float timeInterval);
        void Reset(CollisionWorld collisionWorld);
        void Warp(ref Vector3 origin);
        
        void PreStep(CollisionWorld    collisionWorld);
        void PlayerStep(CollisionWorld collisionWorld, float deltaTime);
        bool CanJump { get; }
        void Jump(Vector3? dir = null);
        
        bool OnGround { get; }
        void SetUpInterpolate(bool value);
	}
}
