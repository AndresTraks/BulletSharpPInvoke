using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Hacd : BulletDisposableObject
	{
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate bool CallbackFunctionUnmanagedDelegate(IntPtr message, double progress, double globalConcavity, IntPtr numVertices);

		public delegate bool CallbackFunction(string message, double progress, double globalConcavity, int numVertices);

		private CallbackFunctionUnmanagedDelegate _callbackFunctionUnmanaged;
		private CallbackFunction _callbackFunction;

		public Hacd()
		{
			IntPtr native = HACD_HACD_new();
			InitializeUserOwned(native);
		}

		private bool CallbackFunctionUnmanaged(IntPtr msg, double progress, double globalConcavity, IntPtr n)
		{
			string msg2 = Marshal.PtrToStringAnsi(msg);
			return _callbackFunction(msg2, progress, globalConcavity, n.ToInt32());
		}

		public bool Compute()
		{
			return HACD_HACD_Compute(Native);
		}

		public bool Compute(bool fullCH)
		{
			return HACD_HACD_Compute2(Native, fullCH);
		}

		public bool Compute(bool fullCH, bool exportDistPoints)
		{
			return HACD_HACD_Compute3(Native, fullCH, exportDistPoints);
		}

		public void DenormalizeData()
		{
			HACD_HACD_DenormalizeData(Native);
		}

		public bool GetCH(int numCH, double[] points, long[] triangles)
		{
			if (points.Length < GetNPointsCH(numCH))
			{
				return false;
			}

			if (triangles.Length < GetNTrianglesCH(numCH))
			{
				return false;
			}

			GCHandle pointsArray = GCHandle.Alloc(points, GCHandleType.Pinned);
			GCHandle trianglesArray = GCHandle.Alloc(triangles, GCHandleType.Pinned);
			bool ret = HACD_HACD_GetCH(Native, numCH, pointsArray.AddrOfPinnedObject(), trianglesArray.AddrOfPinnedObject());
			pointsArray.Free();
			trianglesArray.Free();
			return ret;
		}

		public int GetNPointsCH(int numCH)
		{
			return HACD_HACD_GetNPointsCH(Native, numCH);
		}

		public int GetNTrianglesCH(int numCH)
		{
			return HACD_HACD_GetNTrianglesCH(Native, numCH);
		}

		public double[] GetPoints()
		{
			IntPtr pointsPtr = HACD_HACD_GetPoints(Native);
			int pointsLen = NPoints * 3;
			if (pointsLen == 0 || pointsPtr == IntPtr.Zero)
			{
				return new double[0];
			}
			double[] pointsArray = new double[pointsLen];
			Marshal.Copy(pointsPtr, pointsArray, 0, pointsLen);
			return pointsArray;
		}

		public long[] GetTriangles()
		{
			IntPtr trianglesPtr = HACD_HACD_GetTriangles(Native);
			int trianglesLen = NTriangles * 3;
			if (trianglesLen == 0 || trianglesPtr == IntPtr.Zero)
			{
				return new long[0];
			}
			long[] trianglesArray = new long[trianglesLen];
			Marshal.Copy(trianglesPtr, trianglesArray, 0, trianglesLen);
			return trianglesArray;
		}

		public void NormalizeData()
		{
			HACD_HACD_NormalizeData(Native);
		}

		public bool Save(string fileName, bool uniColor)
		{
			IntPtr filenameTemp = Marshal.StringToHGlobalAnsi(fileName);
			bool ret = HACD_HACD_Save(Native, filenameTemp, uniColor);
			Marshal.FreeHGlobal(filenameTemp);
			return ret;
		}

		public bool Save(string fileName, bool uniColor, long numCluster)
		{
			IntPtr filenameTemp = Marshal.StringToHGlobalAnsi(fileName);
			bool ret = HACD_HACD_Save2(Native, filenameTemp, uniColor, numCluster);
			Marshal.FreeHGlobal(filenameTemp);
			return ret;
		}

		public void SetPoints(ICollection<double> points)
		{
			double[] pointsArray;
			int arrayLen = points.Count;
			pointsArray = points as double[];
			if (pointsArray == null)
			{
				pointsArray = new double[arrayLen];
				points.CopyTo(pointsArray, 0);
			}

			IntPtr pointsPtr = HACD_HACD_GetPoints(Native);
			if (pointsPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(pointsPtr);
			}

			pointsPtr = Marshal.AllocHGlobal(sizeof(double) * arrayLen);
			Marshal.Copy(pointsArray, 0, pointsPtr, arrayLen);
			HACD_HACD_SetPoints(Native, pointsPtr);
			NPoints = arrayLen / 3;
		}

		public void SetPoints(ICollection<Vector3> points)
		{
			double[] pointsArray = new double[points.Count * 3];
			int i = 0;
			foreach (Vector3 v in points)
			{
				pointsArray[i++] = v.X;
				pointsArray[i++] = v.Y;
				pointsArray[i++] = v.Z;
			}
			SetPoints(pointsArray);
		}

		public void SetTriangles(ICollection<long> triangles)
		{
			long[] trianglesLong;
			int arrayLen = triangles.Count;
			trianglesLong = triangles as long[];
			if (trianglesLong == null)
			{
				trianglesLong = new long[arrayLen];
				triangles.CopyTo(trianglesLong, 0);
			}

			IntPtr trianglesPtr = HACD_HACD_GetTriangles(Native);
			if (trianglesPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(trianglesPtr);
			}

			trianglesPtr = Marshal.AllocHGlobal(sizeof(long) * arrayLen);
			Marshal.Copy(trianglesLong, 0, trianglesPtr, arrayLen);
			HACD_HACD_SetTriangles(Native, trianglesPtr);
			NTriangles = arrayLen / 3;
		}

		public void SetTriangles(ICollection<int> triangles)
		{
			int n = triangles.Count;
			long[] trianglesLong = new long[n];
			int i = 0;
			foreach (int t in triangles)
			{
				trianglesLong[i++] = t;
			}
			SetTriangles(trianglesLong);
		}

		public bool AddExtraDistPoints
		{
			get => HACD_HACD_GetAddExtraDistPoints(Native);
			set => HACD_HACD_SetAddExtraDistPoints(Native, value);
		}

		public bool AddFacesPoints
		{
			get => HACD_HACD_GetAddFacesPoints(Native);
			set => HACD_HACD_SetAddFacesPoints(Native, value);
		}

		public bool AddNeighboursDistPoints
		{
			get => HACD_HACD_GetAddNeighboursDistPoints(Native);
			set => HACD_HACD_SetAddNeighboursDistPoints(Native, value);
		}

		public CallbackFunction Callback
		{
			get => _callbackFunction;
			set
			{
				_callbackFunctionUnmanaged = CallbackFunctionUnmanaged;
				_callbackFunction = value;
				if (value != null)
				{
					HACD_HACD_SetCallBack(Native, Marshal.GetFunctionPointerForDelegate(_callbackFunctionUnmanaged));
				}
				else
				{
					HACD_HACD_SetCallBack(Native, IntPtr.Zero);
				}
			}
		}

		public double CompacityWeight
		{
			get => HACD_HACD_GetCompacityWeight(Native);
			set => HACD_HACD_SetCompacityWeight(Native, value);
		}

		public double Concavity
		{
			get => HACD_HACD_GetConcavity(Native);
			set => HACD_HACD_SetConcavity(Native, value);
		}

		public double ConnectDist
		{
			get => HACD_HACD_GetConnectDist(Native);
			set => HACD_HACD_SetConnectDist(Native, value);
		}

		public int NClusters
		{
			get => HACD_HACD_GetNClusters(Native);
			set => HACD_HACD_SetNClusters(Native, value);
		}

		public int NPoints
		{
			get => HACD_HACD_GetNPoints(Native);
			set => HACD_HACD_SetNPoints(Native, value);
		}

		public int NTriangles
		{
			get => HACD_HACD_GetNTriangles(Native);
			set => HACD_HACD_SetNTriangles(Native, value);
		}

		public int VerticesPerConvexHull
		{
			get => HACD_HACD_GetNVerticesPerCH(Native);
			set => HACD_HACD_SetNVerticesPerCH(Native, value);
		}
		/*
		public long Partition
		{
			get => HACD_HACD_GetPartition(Native);
		}
		*/
		public double ScaleFactor
		{
			get => HACD_HACD_GetScaleFactor(Native);
			set => HACD_HACD_SetScaleFactor(Native, value);
		}

		public double VolumeWeight
		{
			get => HACD_HACD_GetVolumeWeight(Native);
			set => HACD_HACD_SetVolumeWeight(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			IntPtr pointsPtr = HACD_HACD_GetPoints(Native);
			if (pointsPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(pointsPtr);
				HACD_HACD_SetPoints(Native, IntPtr.Zero);
			}

			IntPtr trianglesPtr = HACD_HACD_GetTriangles(Native);
			if (trianglesPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(trianglesPtr);
				HACD_HACD_SetTriangles(Native, IntPtr.Zero);
			}

			HACD_HACD_delete(Native);
		}
	}
}
