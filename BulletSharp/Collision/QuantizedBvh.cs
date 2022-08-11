using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class QuantizedBvhNode : BulletDisposableObject
	{
		public QuantizedBvhNode()
		{
			IntPtr native = btQuantizedBvhNode_new();
			InitializeUserOwned(native);
		}

		public int EscapeIndex => btQuantizedBvhNode_getEscapeIndex(Native);

		public int EscapeIndexOrTriangleIndex
		{
			get => btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(Native);
			set => btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(Native, value);
		}

		public bool IsLeafNode => btQuantizedBvhNode_isLeafNode(Native);

		public int PartId => btQuantizedBvhNode_getPartId(Native);
		/*
		public UShortArray QuantizedAabbMax
		{
			get { return btQuantizedBvhNode_getQuantizedAabbMax(Native); }
		}

		public UShortArray QuantizedAabbMin
		{
			get { return btQuantizedBvhNode_getQuantizedAabbMin(Native); }
		}
		*/
		public int TriangleIndex => btQuantizedBvhNode_getTriangleIndex(Native);

		protected override void Dispose(bool disposing)
		{
			btQuantizedBvhNode_delete(Native);
		}
	}

	public class OptimizedBvhNode : BulletDisposableObject
	{
		public OptimizedBvhNode()
		{
			IntPtr native = btOptimizedBvhNode_new();
			InitializeUserOwned(native);
		}

		public Vector3 AabbMaxOrg
		{
			get
			{
				Vector3 value;
				btOptimizedBvhNode_getAabbMaxOrg(Native, out value);
				return value;
			}
			set => btOptimizedBvhNode_setAabbMaxOrg(Native, ref value);
		}

		public Vector3 AabbMinOrg
		{
			get
			{
				Vector3 value;
				btOptimizedBvhNode_getAabbMinOrg(Native, out value);
				return value;
			}
			set => btOptimizedBvhNode_setAabbMinOrg(Native, ref value);
		}

		public int EscapeIndex
		{
			get => btOptimizedBvhNode_getEscapeIndex(Native);
			set => btOptimizedBvhNode_setEscapeIndex(Native, value);
		}

		public int SubPart
		{
			get => btOptimizedBvhNode_getSubPart(Native);
			set => btOptimizedBvhNode_setSubPart(Native, value);
		}

		public int TriangleIndex
		{
			get => btOptimizedBvhNode_getTriangleIndex(Native);
			set => btOptimizedBvhNode_setTriangleIndex(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			btOptimizedBvhNode_delete(Native);
		}
	}

	public abstract class NodeOverlapCallback : BulletDisposableObject
	{
		internal NodeOverlapCallback(IntPtr native) // public
		{
		}

		public void ProcessNode(int subPart, int triangleIndex)
		{
			btNodeOverlapCallback_processNode(Native, subPart, triangleIndex);
		}

		protected override void Dispose(bool disposing)
		{
			btNodeOverlapCallback_delete(Native);
		}
	}

	public class QuantizedBvh : BulletDisposableObject
	{
		public enum TraversalMode
		{
			Stackless,
			StacklessCacheFriendly,
			Recursive
		}

		internal QuantizedBvh(ConstructionInfo info)
		{
		}

		public QuantizedBvh()
		{
			IntPtr native = btQuantizedBvh_new();
			InitializeUserOwned(native);
		}

		public void BuildInternal()
		{
			btQuantizedBvh_buildInternal(Native);
		}

		public uint CalculateSerializeBufferSize()
		{
			return btQuantizedBvh_calculateSerializeBufferSize(Native);
		}

		public int CalculateSerializeBufferSizeNew()
		{
			return btQuantizedBvh_calculateSerializeBufferSizeNew(Native);
		}

		public void DeSerializeDouble(IntPtr quantizedBvhDoubleData)
		{
			btQuantizedBvh_deSerializeDouble(Native, quantizedBvhDoubleData);
		}

		public void DeSerializeFloat(IntPtr quantizedBvhFloatData)
		{
			btQuantizedBvh_deSerializeFloat(Native, quantizedBvhFloatData);
		}
		/*
		public static QuantizedBvh DeSerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize,
			bool swapEndian)
		{
			return btQuantizedBvh_deSerializeInPlace(alignedDataBuffer, dataBufferSize,
				swapEndian);
		}

		public void Quantize(unsigned short out, Vector3 point, int isMax)
		{
			btQuantizedBvh_quantize(Native, out._native, ref point, isMax);
		}

		public void QuantizeWithClamp(unsigned short out, Vector3 point2, int isMax)
		{
			btQuantizedBvh_quantizeWithClamp(Native, out._native, ref point2, isMax);
		}
		*/
		public void ReportAabbOverlappingNodex(NodeOverlapCallback nodeCallback,
			Vector3 aabbMin, Vector3 aabbMax)
		{
			btQuantizedBvh_reportAabbOverlappingNodex(Native, nodeCallback.Native,
				ref aabbMin, ref aabbMax);
		}

		public void ReportBoxCastOverlappingNodex(NodeOverlapCallback nodeCallback,
			Vector3 raySource, Vector3 rayTarget, Vector3 aabbMin, Vector3 aabbMax)
		{
			btQuantizedBvh_reportBoxCastOverlappingNodex(Native, nodeCallback.Native,
				ref raySource, ref rayTarget, ref aabbMin, ref aabbMax);
		}

		public void ReportRayOverlappingNodex(NodeOverlapCallback nodeCallback, Vector3 raySource,
			Vector3 rayTarget)
		{
			btQuantizedBvh_reportRayOverlappingNodex(Native, nodeCallback.Native,
				ref raySource, ref rayTarget);
		}

		public bool Serialize(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
			return btQuantizedBvh_serialize(Native, alignedDataBuffer, dataBufferSize,
				swapEndian);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btQuantizedBvh_serialize2(Native, dataBuffer, serializer.Native));
		}

		public void SetQuantizationValues(Vector3 bvhAabbMin, Vector3 bvhAabbMax,
			float quantizationMargin = 1.0f)
		{
			btQuantizedBvh_setQuantizationValues(Native, ref bvhAabbMin, ref bvhAabbMax,
				quantizationMargin);
		}

		public void SetTraversalMode(TraversalMode traversalMode)
		{
			btQuantizedBvh_setTraversalMode(Native, traversalMode);
		}
		/*
		public Vector3 UnQuantize(unsigned short vecIn)
		{
			Vector3 value;
			btQuantizedBvh_unQuantize(Native, vecIn._native, out value);
			return value;
		}
		*/
		public static uint AlignmentSerializationPadding => btQuantizedBvh_getAlignmentSerializationPadding();

		public bool IsQuantized => btQuantizedBvh_isQuantized(Native);
		/*
		public QuantizedNodeArray LeafNodeArray
		{
			get { return btQuantizedBvh_getLeafNodeArray(Native); }
		}

		public QuantizedNodeArray QuantizedNodeArray
		{
			get { return btQuantizedBvh_getQuantizedNodeArray(Native); }
		}

		public BvhSubtreeInfoArray SubtreeInfoArray
		{
			get { return btQuantizedBvh_getSubtreeInfoArray(Native); }
		}
		*/
		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btQuantizedBvh_delete(Native);
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct QuantizedBvhFloatData
	{
		public Vector3FloatData BvhAabbMin;
		public Vector3FloatData BvhAabbMax;
		public Vector3FloatData BvhQuantization;
		public int CurNodeIndex;
		public int UseQuantization;
		public int NumContiguousLeafNodes;
		public int NumQuantizedContiguousNodes;
		public IntPtr ContiguousNodesPtr;
		public IntPtr QuantizedContiguousNodesPtr;
		public IntPtr SubTreeInfoPtr;
		public int TraversalMode;
		public int NumSubtreeHeaders;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(QuantizedBvhFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct QuantizedBvhDoubleData
	{
		public Vector3DoubleData BvhAabbMin;
		public Vector3DoubleData BvhAabbMax;
		public Vector3DoubleData BvhQuantization;
		public int CurNodeIndex;
		public int UseQuantization;
		public int NumContiguousLeafNodes;
		public int NumQuantizedContiguousNodes;
		public IntPtr ContiguousNodesPtr;
		public IntPtr QuantizedContiguousNodesPtr;
		public IntPtr SubTreeInfoPtr;
		public int TraversalMode;
		public int NumSubtreeHeaders;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(QuantizedBvhDoubleData), fieldName).ToInt32(); }
	}
}
