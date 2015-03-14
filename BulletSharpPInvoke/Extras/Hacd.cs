using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class Hacd : IDisposable
	{
		internal IntPtr _native;

		public Hacd()
		{
			_native = HACD_new();
		}

		public bool Compute()
		{
			return HACD_Compute(_native);
		}

		public bool Compute(bool fullCH)
		{
			return HACD_Compute2(_native, fullCH);
		}

		public bool Compute(bool fullCH, bool exportDistPoints)
		{
			return HACD_Compute3(_native, fullCH, exportDistPoints);
		}

		public void DenormalizeData()
		{
			HACD_DenormalizeData(_native);
		}

        public bool GetCH(int numCH, out Vector3[] points, out int[] triangles)
		{
            throw new NotImplementedException();
			//return HACD_GetCH(_native, numCH, points, triangles);
		}

		public int GetNPointsCH(int numCH)
		{
			return HACD_GetNPointsCH(_native, numCH);
		}

		public int GetNTrianglesCH(int numCH)
		{
			return HACD_GetNTrianglesCH(_native, numCH);
		}

		public void NormalizeData()
		{
			HACD_NormalizeData(_native);
		}

		public bool Save(string fileName, bool uniColor)
		{
            IntPtr filenameTemp = Marshal.StringToHGlobalAnsi(fileName);
            bool ret = HACD_Save(_native, filenameTemp, uniColor);
            Marshal.FreeHGlobal(filenameTemp);
            return ret;
		}

        public bool Save(string fileName, bool uniColor, long numCluster)
		{
            IntPtr filenameTemp = Marshal.StringToHGlobalAnsi(fileName);
            bool ret = HACD_Save2(_native, filenameTemp, uniColor, numCluster);
            Marshal.FreeHGlobal(filenameTemp);
            return ret;
		}

		public bool AddExtraDistPoints
		{
			get { return HACD_GetAddExtraDistPoints(_native); }
			set { HACD_SetAddExtraDistPoints(_native, value); }
		}

		public bool AddFacesPoints
		{
			get { return HACD_GetAddFacesPoints(_native); }
			set { HACD_SetAddFacesPoints(_native, value); }
		}

		public bool AddNeighboursDistPoints
		{
			get { return HACD_GetAddNeighboursDistPoints(_native); }
			set { HACD_SetAddNeighboursDistPoints(_native, value); }
		}
        /*
		public CallBackFunction CallBack
		{
			get { return HACD_GetCallBack(_native); }
			set { HACD_SetCallBack(_native, value._native); }
		}
        */
		public double CompacityWeight
		{
			get { return HACD_GetCompacityWeight(_native); }
			set { HACD_SetCompacityWeight(_native, value); }
		}

		public double Concavity
		{
			get { return HACD_GetConcavity(_native); }
			set { HACD_SetConcavity(_native, value); }
		}

		public double ConnectDist
		{
			get { return HACD_GetConnectDist(_native); }
			set { HACD_SetConnectDist(_native, value); }
		}

		public int NClusters
		{
			get { return HACD_GetNClusters(_native); }
			set { HACD_SetNClusters(_native, value); }
		}

		public int NPoints
		{
			get { return HACD_GetNPoints(_native); }
			set { HACD_SetNPoints(_native, value); }
		}

		public int NTriangles
		{
			get { return HACD_GetNTriangles(_native); }
			set { HACD_SetNTriangles(_native, value); }
		}

        public int NumVerticesPerConvexHull
		{
			get { return HACD_GetNVerticesPerCH(_native); }
			set { HACD_SetNVerticesPerCH(_native, value); }
		}
        /*
		public long Partition
		{
			get { return HACD_GetPartition(_native); }
		}

		public HACD::Vec3 Points
		{
			get { return HACD_GetPoints(_native); }
			set { HACD_SetPoints(_native, value._native); }
		}
        */
		public double ScaleFactor
		{
			get { return HACD_GetScaleFactor(_native); }
			set { HACD_SetScaleFactor(_native, value); }
		}
        /*
		public HACD::Vec3 Triangles
		{
			get { return HACD_GetTriangles(_native); }
			set { HACD_SetTriangles(_native, value._native); }
		}
        */
		public double VolumeWeight
		{
			get { return HACD_GetVolumeWeight(_native); }
			set { HACD_SetVolumeWeight(_native, value); }
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
				HACD_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Hacd()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr HACD_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_Compute(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_Compute2(IntPtr obj, bool fullCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_Compute3(IntPtr obj, bool fullCH, bool exportDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_DenormalizeData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_GetAddExtraDistPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_GetAddFacesPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_GetAddNeighboursDistPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr HACD_GetCallBack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_GetCH(IntPtr obj, int numCH, IntPtr points, IntPtr triangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern double HACD_GetCompacityWeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern double HACD_GetConcavity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern double HACD_GetConnectDist(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int HACD_GetNClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int HACD_GetNPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int HACD_GetNPointsCH(IntPtr obj, int numCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int HACD_GetNTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int HACD_GetNTrianglesCH(IntPtr obj, int numCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int HACD_GetNVerticesPerCH(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr HACD_GetPartition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr HACD_GetPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern double HACD_GetScaleFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr HACD_GetTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern double HACD_GetVolumeWeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_NormalizeData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_Save(IntPtr obj, IntPtr fileName, bool uniColor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool HACD_Save2(IntPtr obj, IntPtr fileName, bool uniColor, long numCluster);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetAddExtraDistPoints(IntPtr obj, bool addExtraDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetAddFacesPoints(IntPtr obj, bool addFacesPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetAddNeighboursDistPoints(IntPtr obj, bool addNeighboursDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetCallBack(IntPtr obj, IntPtr callBack);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetCompacityWeight(IntPtr obj, double alpha);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetConcavity(IntPtr obj, double concavity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetConnectDist(IntPtr obj, double ccConnectDist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetNClusters(IntPtr obj, int nClusters);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetNPoints(IntPtr obj, int nPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetNTriangles(IntPtr obj, int nTriangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetNVerticesPerCH(IntPtr obj, int nVerticesPerCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetPoints(IntPtr obj, IntPtr points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetScaleFactor(IntPtr obj, double scale);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetTriangles(IntPtr obj, IntPtr triangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_SetVolumeWeight(IntPtr obj, double beta);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void HACD_delete(IntPtr obj);
	}
}
