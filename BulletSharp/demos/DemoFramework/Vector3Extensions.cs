using System;
using System.Numerics;

namespace DemoFramework
{
    public static class Vector3Extensions
    {
        public static float GetComponent(this Vector3 value, int index)
        {
            return index switch
            {
                0 => value.X,
                1 => value.Y,
                2 => value.Z,
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };
        }
    }
}
