using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CharacterControllerInterfaceExtensions
	{
		public unsafe static void SetWalkDirection(this ICharacterController obj, ref OpenTK.Vector3 walkDirection)
		{
			fixed (OpenTK.Vector3* walkDirectionPtr = &walkDirection)
			{
				obj.SetWalkDirection(ref *(BulletSharp.Math.Vector3*)walkDirectionPtr);
			}
		}

        public unsafe static void SetVelocityForTimeInterval(this ICharacterController obj, ref OpenTK.Vector3 velocity, float timeInterval)
		{
			fixed (OpenTK.Vector3* velocityPtr = &velocity)
			{
				obj.SetVelocityForTimeInterval(ref *(BulletSharp.Math.Vector3*)velocityPtr, timeInterval);
			}
		}

        public unsafe static void Warp(this ICharacterController obj, ref OpenTK.Vector3 origin)
		{
			fixed (OpenTK.Vector3* originPtr = &origin)
			{
				obj.Warp(ref *(BulletSharp.Math.Vector3*)originPtr);
			}
		}
	}
}
