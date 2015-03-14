using SiliconStudio.Core.Mathematics;
﻿using System;
using System.Collections;

namespace BulletSharp
{
    public abstract class AlignedObjectArray
    {
        internal IntPtr _native;

        internal AlignedObjectArray(IntPtr native)
        {
            _native = native;
        }
    }
}
