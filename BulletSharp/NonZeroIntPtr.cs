using System;

namespace BulletSharp
{
	/// <summary>
	/// Struct will throw exception when assigning or accessing zero'ed pointer
	/// </summary>
	public struct NonZeroIntPtr
	{
		IntPtr ptr;

		/// <summary>
		/// Assign the following IntPtr to this struct
		/// </summary>
		/// <param name="ptrParam">The value to pass over</param>
		/// <param name="testZeroed">Should we throw if the pointed is zero'ed</param>
		public NonZeroIntPtr(IntPtr ptrParam)
		{
			if (ptrParam == IntPtr.Zero)
				throw new ArgumentNullException(nameof(ptrParam));
			ptr = ptrParam;
		}

		/// <summary>
		/// Same as implicit casting
		/// </summary>
		public IntPtr Pointer
		{
			get{ return this; }
		}

		/// <summary>
		/// Is the pointer zero'ed, doesn't throw
		/// </summary>
		public bool IsZero()
		{
			return ptr == IntPtr.Zero;
		}

		public static implicit operator NonZeroIntPtr(IntPtr v)
		{
			return new NonZeroIntPtr(v);
		}

		public static implicit operator IntPtr(NonZeroIntPtr v)
		{
			return v.ptr == IntPtr.Zero ? throw new ArgumentNullException("Pointer") : v.ptr;
		}
		
		/// <summary>
		/// Return false if the pointer is zeroed, outputs the pointer and set the pointer inside this one to zero
		/// </summary>
		public bool Clear( out IntPtr intPtr )
		{
			intPtr = ptr;
			ptr = IntPtr.Zero;
			return IsZero() == false;
		}
	}
}
