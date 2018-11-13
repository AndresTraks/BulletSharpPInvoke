using System;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class TriangleInfo : BulletDisposableObject
	{
		public TriangleInfo()
		{
			IntPtr native = btTriangleInfo_new();
			InitializeUserOwned(native);
		}

		public float EdgeV0V1Angle
		{
			get => btTriangleInfo_getEdgeV0V1Angle(Native);
			set => btTriangleInfo_setEdgeV0V1Angle(Native, value);
		}

		public float EdgeV1V2Angle
		{
			get => btTriangleInfo_getEdgeV1V2Angle(Native);
			set => btTriangleInfo_setEdgeV1V2Angle(Native, value);
		}

		public float EdgeV2V0Angle
		{
			get => btTriangleInfo_getEdgeV2V0Angle(Native);
			set => btTriangleInfo_setEdgeV2V0Angle(Native, value);
		}

		public int Flags
		{
			get => btTriangleInfo_getFlags(Native);
			set => btTriangleInfo_setFlags(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			btTriangleInfo_delete(Native);
		}
	}

	public class TriangleInfoMap : BulletDisposableObject
	{
		internal TriangleInfoMap(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public TriangleInfoMap()
		{
			IntPtr native = btTriangleInfoMap_new();
			InitializeUserOwned(native);
		}

		public int CalculateSerializeBufferSize()
		{
			return btTriangleInfoMap_calculateSerializeBufferSize(Native);
		}
		/*
		public void DeSerialize(TriangleInfoMapData data)
		{
			btTriangleInfoMap_deSerialize(Native, data._native);
		}
		*/
		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btTriangleInfoMap_serialize(Native, dataBuffer, serializer.Native));
		}

		public float ConvexEpsilon
		{
			get => btTriangleInfoMap_getConvexEpsilon(Native);
			set => btTriangleInfoMap_setConvexEpsilon(Native, value);
		}

		public float EdgeDistanceThreshold
		{
			get => btTriangleInfoMap_getEdgeDistanceThreshold(Native);
			set => btTriangleInfoMap_setEdgeDistanceThreshold(Native, value);
		}

		public float EqualVertexThreshold
		{
			get => btTriangleInfoMap_getEqualVertexThreshold(Native);
			set => btTriangleInfoMap_setEqualVertexThreshold(Native, value);
		}

		public float MaxEdgeAngleThreshold
		{
			get => btTriangleInfoMap_getMaxEdgeAngleThreshold(Native);
			set => btTriangleInfoMap_setMaxEdgeAngleThreshold(Native, value);
		}

		public float PlanarEpsilon
		{
			get => btTriangleInfoMap_getPlanarEpsilon(Native);
			set => btTriangleInfoMap_setPlanarEpsilon(Native, value);
		}

		public float ZeroAreaThreshold
		{
			get => btTriangleInfoMap_getZeroAreaThreshold(Native);
			set => btTriangleInfoMap_setZeroAreaThreshold(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btTriangleInfoMap_delete(Native);
			}
		}
	}
}
