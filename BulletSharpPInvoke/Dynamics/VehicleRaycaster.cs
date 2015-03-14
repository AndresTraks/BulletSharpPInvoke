using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class VehicleRaycasterResult
    {
        public float DistFraction { get; set; }
        public Vector3 HitNormalInWorld { get; set; }
        public Vector3 HitPointInWorld { get; set; }
    }
    
    public interface IVehicleRaycaster
	{
        Object CastRay(ref Vector3 from, ref Vector3 to, VehicleRaycasterResult result);
	}
}
