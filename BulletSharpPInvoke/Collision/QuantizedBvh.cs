using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class QuantizedBvhNode : IDisposable
	{
		internal IntPtr _native;

		internal QuantizedBvhNode(IntPtr native)
		{
			_native = native;
		}

		public QuantizedBvhNode()
		{
			_native = btQuantizedBvhNode_new();
		}

		public int EscapeIndex
		{
			get { return btQuantizedBvhNode_getEscapeIndex(_native); }
		}

		public int EscapeIndexOrTriangleIndex
		{
			get { return btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(_native); }
			set { btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(_native, value); }
		}

		public bool IsLeafNode
		{
			get { return btQuantizedBvhNode_isLeafNode(_native); }
		}

		public int PartId
		{
			get { return btQuantizedBvhNode_getPartId(_native); }
		}
        /*
		public UShortArray QuantizedAabbMax
		{
			get { return btQuantizedBvhNode_getQuantizedAabbMax(_native); }
		}

		public UShortArray QuantizedAabbMin
		{
			get { return btQuantizedBvhNode_getQuantizedAabbMin(_native); }
		}
        */
		public int TriangleIndex
		{
			get { return btQuantizedBvhNode_getTriangleIndex(_native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btQuantizedBvhNode_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~QuantizedBvhNode()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvhNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhNode_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhNode_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvhNode_getQuantizedAabbMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvhNode_getQuantizedAabbMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhNode_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btQuantizedBvhNode_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhNode_delete(IntPtr obj);
	}

	public class OptimizedBvhNode : IDisposable
	{
		internal IntPtr _native;

		internal OptimizedBvhNode(IntPtr native)
		{
			_native = native;
		}

		public OptimizedBvhNode()
		{
			_native = btOptimizedBvhNode_new();
		}

		public Vector3 AabbMaxOrg
		{
			get
			{
				Vector3 value;
				btOptimizedBvhNode_getAabbMaxOrg(_native, out value);
				return value;
			}
			set { btOptimizedBvhNode_setAabbMaxOrg(_native, ref value); }
		}

		public Vector3 AabbMinOrg
		{
			get
			{
				Vector3 value;
				btOptimizedBvhNode_getAabbMinOrg(_native, out value);
				return value;
			}
			set { btOptimizedBvhNode_setAabbMinOrg(_native, ref value); }
		}

		public int EscapeIndex
		{
			get { return btOptimizedBvhNode_getEscapeIndex(_native); }
			set { btOptimizedBvhNode_setEscapeIndex(_native, value); }
		}

		public int SubPart
		{
			get { return btOptimizedBvhNode_getSubPart(_native); }
			set { btOptimizedBvhNode_setSubPart(_native, value); }
		}

		public int TriangleIndex
		{
			get { return btOptimizedBvhNode_getTriangleIndex(_native); }
			set { btOptimizedBvhNode_setTriangleIndex(_native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btOptimizedBvhNode_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~OptimizedBvhNode()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOptimizedBvhNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_getAabbMaxOrg(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_getAabbMinOrg(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btOptimizedBvhNode_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOptimizedBvhNode_getPadding(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btOptimizedBvhNode_getSubPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btOptimizedBvhNode_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_setAabbMaxOrg(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_setAabbMinOrg(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_setEscapeIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_setPadding(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_setSubPart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvhNode_delete(IntPtr obj);
	}
    /*
	public class BvhSubtreeInfo : IDisposable
	{
		internal IntPtr _native;

		internal BvhSubtreeInfo(IntPtr native)
		{
			_native = native;
		}

		public BvhSubtreeInfo()
		{
			_native = btBvhSubtreeInfo_new();
		}

		public void SetAabbFromQuantizeNode(QuantizedBvhNode quantizedNode)
		{
			btBvhSubtreeInfo_setAabbFromQuantizeNode(_native, quantizedNode._native);
		}

		public int Padding
		{
			get { return btBvhSubtreeInfo_getPadding(_native); }
			set { btBvhSubtreeInfo_setPadding(_native, value._native); }
		}

		public unsigned short QuantizedAabbMax
		{
			get { return btBvhSubtreeInfo_getQuantizedAabbMax(_native); }
			set { btBvhSubtreeInfo_setQuantizedAabbMax(_native, value._native); }
		}

		public unsigned short QuantizedAabbMin
		{
			get { return btBvhSubtreeInfo_getQuantizedAabbMin(_native); }
			set { btBvhSubtreeInfo_setQuantizedAabbMin(_native, value._native); }
		}

		public int RootNodeIndex
		{
			get { return btBvhSubtreeInfo_getRootNodeIndex(_native); }
			set { btBvhSubtreeInfo_setRootNodeIndex(_native, value); }
		}

		public int SubtreeSize
		{
			get { return btBvhSubtreeInfo_getSubtreeSize(_native); }
			set { btBvhSubtreeInfo_setSubtreeSize(_native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btBvhSubtreeInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BvhSubtreeInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhSubtreeInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhSubtreeInfo_getPadding(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhSubtreeInfo_getQuantizedAabbMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhSubtreeInfo_getQuantizedAabbMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btBvhSubtreeInfo_getRootNodeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btBvhSubtreeInfo_getSubtreeSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_setAabbFromQuantizeNode(IntPtr obj, IntPtr quantizedNode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_setPadding(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_setQuantizedAabbMax(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_setQuantizedAabbMin(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_setRootNodeIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_setSubtreeSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhSubtreeInfo_delete(IntPtr obj);
	}
    */
	public class NodeOverlapCallback : IDisposable
	{
		internal IntPtr _native;

		internal NodeOverlapCallback(IntPtr native)
		{
			_native = native;
		}

		public void ProcessNode(int subPart, int triangleIndex)
		{
			btNodeOverlapCallback_processNode(_native, subPart, triangleIndex);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btNodeOverlapCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~NodeOverlapCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btNodeOverlapCallback_processNode(IntPtr obj, int subPart, int triangleIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btNodeOverlapCallback_delete(IntPtr obj);
	}

	public class QuantizedBvh : IDisposable
	{
        public enum TraversalMode
		{
			Stackless,
			StacklessCacheFriendly,
			Recursive
		}

		internal IntPtr _native;

		internal QuantizedBvh(IntPtr native)
		{
			_native = native;
		}

		public QuantizedBvh()
		{
			_native = btQuantizedBvh_new();
		}

		public void BuildInternal()
		{
			btQuantizedBvh_buildInternal(_native);
		}

		public uint CalculateSerializeBufferSize()
		{
			return btQuantizedBvh_calculateSerializeBufferSize(_native);
		}

		public int CalculateSerializeBufferSizeNew()
		{
			return btQuantizedBvh_calculateSerializeBufferSizeNew(_native);
		}

        public void DeSerializeDouble(IntPtr quantizedBvhDoubleData)
		{
			btQuantizedBvh_deSerializeDouble(_native, quantizedBvhDoubleData);
		}

        public void DeSerializeFloat(IntPtr quantizedBvhFloatData)
		{
			btQuantizedBvh_deSerializeFloat(_native, quantizedBvhFloatData);
		}
        /*
		public static QuantizedBvh DeSerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
			return btQuantizedBvh_deSerializeInPlace(alignedDataBuffer, dataBufferSize, swapEndian);
		}

		public void Quantize(unsigned short out, Vector3 point, int isMax)
		{
			btQuantizedBvh_quantize(_native, out._native, ref point, isMax);
		}

		public void QuantizeWithClamp(unsigned short out, Vector3 point2, int isMax)
		{
			btQuantizedBvh_quantizeWithClamp(_native, out._native, ref point2, isMax);
		}
        */
		public void ReportAabbOverlappingNodex(NodeOverlapCallback nodeCallback, Vector3 aabbMin, Vector3 aabbMax)
		{
			btQuantizedBvh_reportAabbOverlappingNodex(_native, nodeCallback._native, ref aabbMin, ref aabbMax);
		}

		public void ReportBoxCastOverlappingNodex(NodeOverlapCallback nodeCallback, Vector3 raySource, Vector3 rayTarget, Vector3 aabbMin, Vector3 aabbMax)
		{
			btQuantizedBvh_reportBoxCastOverlappingNodex(_native, nodeCallback._native, ref raySource, ref rayTarget, ref aabbMin, ref aabbMax);
		}

		public void ReportRayOverlappingNodex(NodeOverlapCallback nodeCallback, Vector3 raySource, Vector3 rayTarget)
		{
			btQuantizedBvh_reportRayOverlappingNodex(_native, nodeCallback._native, ref raySource, ref rayTarget);
		}

		public bool Serialize(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
			return btQuantizedBvh_serialize(_native, alignedDataBuffer, dataBufferSize, swapEndian);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btQuantizedBvh_serialize2(_native, dataBuffer, serializer._native));
		}

		public void SetQuantizationValues(Vector3 bvhAabbMin, Vector3 bvhAabbMax)
		{
			btQuantizedBvh_setQuantizationValues(_native, ref bvhAabbMin, ref bvhAabbMax);
		}

		public void SetQuantizationValues(Vector3 bvhAabbMin, Vector3 bvhAabbMax, float quantizationMargin)
		{
			btQuantizedBvh_setQuantizationValues2(_native, ref bvhAabbMin, ref bvhAabbMax, quantizationMargin);
		}

		public void SetTraversalMode(TraversalMode traversalMode)
		{
			btQuantizedBvh_setTraversalMode(_native, traversalMode);
		}
        /*
		public Vector3 UnQuantize(unsigned short vecIn)
		{
			Vector3 value;
			btQuantizedBvh_unQuantize(_native, vecIn._native, out value);
			return value;
		}
        */
		public static uint AlignmentSerializationPadding
		{
			get { return btQuantizedBvh_getAlignmentSerializationPadding(); }
		}

		public bool IsQuantized
		{
			get { return btQuantizedBvh_isQuantized(_native); }
		}
        /*
		public QuantizedNodeArray LeafNodeArray
		{
			get { return btQuantizedBvh_getLeafNodeArray(_native); }
		}

		public QuantizedNodeArray QuantizedNodeArray
		{
			get { return btQuantizedBvh_getQuantizedNodeArray(_native); }
		}

		public BvhSubtreeInfoArray SubtreeInfoArray
		{
			get { return btQuantizedBvh_getSubtreeInfoArray(_native); }
		}
        */
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btQuantizedBvh_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~QuantizedBvh()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_buildInternal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btQuantizedBvh_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvh_calculateSerializeBufferSizeNew(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_deSerializeDouble(IntPtr obj, IntPtr quantizedBvhDoubleData);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_deSerializeFloat(IntPtr obj, IntPtr quantizedBvhFloatData);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvh_deSerializeInPlace(IntPtr i_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btQuantizedBvh_getAlignmentSerializationPadding();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvh_getLeafNodeArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvh_getQuantizedNodeArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvh_getSubtreeInfoArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btQuantizedBvh_isQuantized(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btQuantizedBvh_quantize(IntPtr obj, IntPtr out, [In] ref Vector3 point, int isMax);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btQuantizedBvh_quantizeWithClamp(IntPtr obj, IntPtr out, [In] ref Vector3 point2, int isMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_reportAabbOverlappingNodex(IntPtr obj, IntPtr nodeCallback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_reportBoxCastOverlappingNodex(IntPtr obj, IntPtr nodeCallback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_reportRayOverlappingNodex(IntPtr obj, IntPtr nodeCallback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btQuantizedBvh_serialize(IntPtr obj, IntPtr o_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvh_serialize2(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_setQuantizationValues(IntPtr obj, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_setQuantizationValues2(IntPtr obj, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, float quantizationMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_setTraversalMode(IntPtr obj, TraversalMode traversalMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_unQuantize(IntPtr obj, IntPtr vecIn, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvh_delete(IntPtr obj);
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
}
