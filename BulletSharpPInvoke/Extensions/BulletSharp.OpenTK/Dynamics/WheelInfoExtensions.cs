using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WheelInfoConstructionInfoExtensions
	{
		public unsafe static void GetChassisConnectionCS(this WheelInfoConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ChassisConnectionCS;
			}
		}

		public static OpenTK.Vector3 GetChassisConnectionCS(this WheelInfoConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetChassisConnectionCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelAxleCS(this WheelInfoConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelAxleCS;
			}
		}

		public static OpenTK.Vector3 GetWheelAxleCS(this WheelInfoConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelAxleCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelDirectionCS(this WheelInfoConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelDirectionCS;
			}
		}

		public static OpenTK.Vector3 GetWheelDirectionCS(this WheelInfoConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelDirectionCS(obj, out value);
			return value;
		}

		public unsafe static void SetChassisConnectionCS(this WheelInfoConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ChassisConnectionCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetChassisConnectionCS(this WheelInfoConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetChassisConnectionCS(obj, ref value);
		}

		public unsafe static void SetWheelAxleCS(this WheelInfoConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelAxleCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelAxleCS(this WheelInfoConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetWheelAxleCS(obj, ref value);
		}

		public unsafe static void SetWheelDirectionCS(this WheelInfoConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelDirectionCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelDirectionCS(this WheelInfoConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetWheelDirectionCS(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RaycastInfoExtensions
	{
		public unsafe static void GetContactNormalWS(this RaycastInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ContactNormalWS;
			}
		}

		public static OpenTK.Vector3 GetContactNormalWS(this RaycastInfo obj)
		{
			OpenTK.Vector3 value;
			GetContactNormalWS(obj, out value);
			return value;
		}

		public unsafe static void GetContactPointWS(this RaycastInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ContactPointWS;
			}
		}

		public static OpenTK.Vector3 GetContactPointWS(this RaycastInfo obj)
		{
			OpenTK.Vector3 value;
			GetContactPointWS(obj, out value);
			return value;
		}

		public unsafe static void GetHardPointWS(this RaycastInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HardPointWS;
			}
		}

		public static OpenTK.Vector3 GetHardPointWS(this RaycastInfo obj)
		{
			OpenTK.Vector3 value;
			GetHardPointWS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelAxleWS(this RaycastInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelAxleWS;
			}
		}

		public static OpenTK.Vector3 GetWheelAxleWS(this RaycastInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelAxleWS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelDirectionWS(this RaycastInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelDirectionWS;
			}
		}

		public static OpenTK.Vector3 GetWheelDirectionWS(this RaycastInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelDirectionWS(obj, out value);
			return value;
		}

		public unsafe static void SetContactNormalWS(this RaycastInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ContactNormalWS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetContactNormalWS(this RaycastInfo obj, OpenTK.Vector3 value)
		{
			SetContactNormalWS(obj, ref value);
		}

		public unsafe static void SetContactPointWS(this RaycastInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ContactPointWS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetContactPointWS(this RaycastInfo obj, OpenTK.Vector3 value)
		{
			SetContactPointWS(obj, ref value);
		}

		public unsafe static void SetHardPointWS(this RaycastInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HardPointWS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHardPointWS(this RaycastInfo obj, OpenTK.Vector3 value)
		{
			SetHardPointWS(obj, ref value);
		}

		public unsafe static void SetWheelAxleWS(this RaycastInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelAxleWS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelAxleWS(this RaycastInfo obj, OpenTK.Vector3 value)
		{
			SetWheelAxleWS(obj, ref value);
		}

		public unsafe static void SetWheelDirectionWS(this RaycastInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelDirectionWS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelDirectionWS(this RaycastInfo obj, OpenTK.Vector3 value)
		{
			SetWheelDirectionWS(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WheelInfoExtensions
	{
		public unsafe static void GetChassisConnectionPointCS(this WheelInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ChassisConnectionPointCS;
			}
		}

		public static OpenTK.Vector3 GetChassisConnectionPointCS(this WheelInfo obj)
		{
			OpenTK.Vector3 value;
			GetChassisConnectionPointCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelAxleCS(this WheelInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelAxleCS;
			}
		}

		public static OpenTK.Vector3 GetWheelAxleCS(this WheelInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelAxleCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelDirectionCS(this WheelInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelDirectionCS;
			}
		}

		public static OpenTK.Vector3 GetWheelDirectionCS(this WheelInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelDirectionCS(obj, out value);
			return value;
		}

		public unsafe static void GetWorldTransform(this WheelInfo obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.WorldTransform;
			}
		}

		public static OpenTK.Matrix4 GetWorldTransform(this WheelInfo obj)
		{
			OpenTK.Matrix4 value;
			GetWorldTransform(obj, out value);
			return value;
		}

		public unsafe static void SetChassisConnectionPointCS(this WheelInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ChassisConnectionPointCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetChassisConnectionPointCS(this WheelInfo obj, OpenTK.Vector3 value)
		{
			SetChassisConnectionPointCS(obj, ref value);
		}

		public unsafe static void SetWheelAxleCS(this WheelInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelAxleCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelAxleCS(this WheelInfo obj, OpenTK.Vector3 value)
		{
			SetWheelAxleCS(obj, ref value);
		}

		public unsafe static void SetWheelDirectionCS(this WheelInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelDirectionCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelDirectionCS(this WheelInfo obj, OpenTK.Vector3 value)
		{
			SetWheelDirectionCS(obj, ref value);
		}

		public unsafe static void SetWorldTransform(this WheelInfo obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.WorldTransform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetWorldTransform(this WheelInfo obj, OpenTK.Matrix4 value)
		{
			SetWorldTransform(obj, ref value);
		}
	}
}
