#if __iOS__
using MonoTouch;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;

namespace BulletSharp
{
    public class SharpMotionState : IDisposable
    {
        internal IntPtr _native;
        private GCHandle _handle;

        public SharpMotionState()
        {
            _handle = GCHandle.Alloc(this);
            _native = SharpMotionState_new(GCHandle.ToIntPtr(_handle));
        }

        public void Dispose()
        {
            SharpMotionState_delete(_native);
        }

        [UnmanagedFunctionPointer(Native.Conv)]
        delegate void GetWorldTransformUnmanagedDelegate(IntPtr sharpReference, [Out] out Matrix transform);

        [UnmanagedFunctionPointer(Native.Conv)]
        delegate void SetWorldTransformUnmanagedDelegate(IntPtr sharpReference, [In] ref Matrix transform);

#if __iOS__
        [MonoPInvokeCallback(typeof(GetWorldTransformUnmanagedDelegate))]
#endif
        static void InternalGetWorldTransform(IntPtr sharpReference, [Out] out Matrix transform)
        {
            var obj = (SharpMotionState)GCHandle.FromIntPtr(sharpReference).Target;
            obj.GetWorldTransform(out transform);
        }

#if __iOS__
        [MonoPInvokeCallback(typeof(SetWorldTransformUnmanagedDelegate))]
#endif
        static void InternalSetWorldTransform(IntPtr sharpReference, [In] ref Matrix transform)
        {
            var obj = (SharpMotionState)GCHandle.FromIntPtr(sharpReference).Target;
            obj.SetWorldTransform(transform);
        }

        private static readonly GetWorldTransformUnmanagedDelegate GetWorldTransformUnmanaged = InternalGetWorldTransform;
        private static readonly SetWorldTransformUnmanagedDelegate SetWorldTransformUnmanaged = InternalSetWorldTransform;

        static SharpMotionState()
        {
#if !__iOS__
            SharpMotionState_Setup(Marshal.GetFunctionPointerForDelegate(GetWorldTransformUnmanaged), Marshal.GetFunctionPointerForDelegate(SetWorldTransformUnmanaged));
#else
            SharpMotionState_Setup(GetWorldTransformUnmanaged, SetWorldTransformUnmanaged);
#endif
        }

        public virtual void GetWorldTransform(out Matrix transform)
        {
            transform = Matrix.Identity;
        }

        public virtual void SetWorldTransform(Matrix transform)
        {
            
        }

#if !__iOS__
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr SharpMotionState_Setup(IntPtr getWorldTransformCallback, IntPtr setWorldTransformCallback);  
#else
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr SharpMotionState_Setup(GetWorldTransformUnmanagedDelegate getWorldTransformCallback, SetWorldTransformUnmanagedDelegate setWorldTransformCallback); 
#endif

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr SharpMotionState_new(IntPtr sharpReference);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void SharpMotionState_delete(IntPtr obj);
    }
}
