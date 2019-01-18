// GENERATED CODE
using System;
using System.Runtime.CompilerServices;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [System.Serializable]
    public partial struct uint3x4 : System.IEquatable<uint3x4>, IFormattable
    {
        public uint3 c0;
        public uint3 c1;
        public uint3 c2;
        public uint3 c3;

        /// <summary>uint3x4 zero value.</summary>
        public static readonly uint3x4 zero;

        /// <summary>Constructs a uint3x4 matrix from four uint3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(uint3 c0, uint3 c1, uint3 c2, uint3 c3)
        { 
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
        }

        /// <summary>Constructs a uint3x4 matrix from 12 uint values given in row-major order.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(uint m00, uint m01, uint m02, uint m03,
                       uint m10, uint m11, uint m12, uint m13,
                       uint m20, uint m21, uint m22, uint m23)
        { 
            this.c0 = new uint3(m00, m10, m20);
            this.c1 = new uint3(m01, m11, m21);
            this.c2 = new uint3(m02, m12, m22);
            this.c3 = new uint3(m03, m13, m23);
        }

        /// <summary>Constructs a uint3x4 matrix from a single uint value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(uint v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
            this.c3 = v;
        }

        /// <summary>Constructs a uint3x4 matrix from a single bool value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(bool v)
        {
            this.c0 = math.select(new uint3(0u), new uint3(1u), v);
            this.c1 = math.select(new uint3(0u), new uint3(1u), v);
            this.c2 = math.select(new uint3(0u), new uint3(1u), v);
            this.c3 = math.select(new uint3(0u), new uint3(1u), v);
        }

        /// <summary>Constructs a uint3x4 matrix from a bool3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(bool3x4 v)
        {
            this.c0 = math.select(new uint3(0u), new uint3(1u), v.c0);
            this.c1 = math.select(new uint3(0u), new uint3(1u), v.c1);
            this.c2 = math.select(new uint3(0u), new uint3(1u), v.c2);
            this.c3 = math.select(new uint3(0u), new uint3(1u), v.c3);
        }

        /// <summary>Constructs a uint3x4 matrix from a single int value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(int v)
        {
            this.c0 = (uint3)v;
            this.c1 = (uint3)v;
            this.c2 = (uint3)v;
            this.c3 = (uint3)v;
        }

        /// <summary>Constructs a uint3x4 matrix from a int3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(int3x4 v)
        {
            this.c0 = (uint3)v.c0;
            this.c1 = (uint3)v.c1;
            this.c2 = (uint3)v.c2;
            this.c3 = (uint3)v.c3;
        }

        /// <summary>Constructs a uint3x4 matrix from a single float value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(float v)
        {
            this.c0 = (uint3)v;
            this.c1 = (uint3)v;
            this.c2 = (uint3)v;
            this.c3 = (uint3)v;
        }

        /// <summary>Constructs a uint3x4 matrix from a float3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(float3x4 v)
        {
            this.c0 = (uint3)v.c0;
            this.c1 = (uint3)v.c1;
            this.c2 = (uint3)v.c2;
            this.c3 = (uint3)v.c3;
        }

        /// <summary>Constructs a uint3x4 matrix from a single double value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(double v)
        {
            this.c0 = (uint3)v;
            this.c1 = (uint3)v;
            this.c2 = (uint3)v;
            this.c3 = (uint3)v;
        }

        /// <summary>Constructs a uint3x4 matrix from a double3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint3x4(double3x4 v)
        {
            this.c0 = (uint3)v.c0;
            this.c1 = (uint3)v.c1;
            this.c2 = (uint3)v.c2;
            this.c3 = (uint3)v.c3;
        }


        /// <summary>Implicitly converts a single uint value to a uint3x4 matrix by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint3x4(uint v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a single bool value to a uint3x4 matrix by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(bool v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a bool3x4 matrix to a uint3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(bool3x4 v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a single int value to a uint3x4 matrix by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(int v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a int3x4 matrix to a uint3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(int3x4 v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a single float value to a uint3x4 matrix by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(float v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a float3x4 matrix to a uint3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(float3x4 v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a single double value to a uint3x4 matrix by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(double v) { return new uint3x4(v); }

        /// <summary>Explicitly converts a double3x4 matrix to a uint3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint3x4(double3x4 v) { return new uint3x4(v); }


        /// <summary>Returns the result of a componentwise multiplication operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator * (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3); }

        /// <summary>Returns the result of a componentwise multiplication operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator * (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs); }

        /// <summary>Returns the result of a componentwise multiplication operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator * (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3); }


        /// <summary>Returns the result of a componentwise addition operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator + (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3); }

        /// <summary>Returns the result of a componentwise addition operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator + (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs); }

        /// <summary>Returns the result of a componentwise addition operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator + (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3); }


        /// <summary>Returns the result of a componentwise subtraction operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator - (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3); }

        /// <summary>Returns the result of a componentwise subtraction operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator - (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs); }

        /// <summary>Returns the result of a componentwise subtraction operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator - (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3); }


        /// <summary>Returns the result of a componentwise division operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator / (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3); }

        /// <summary>Returns the result of a componentwise division operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator / (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs); }

        /// <summary>Returns the result of a componentwise division operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator / (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3); }


        /// <summary>Returns the result of a componentwise modulus operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator % (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3); }

        /// <summary>Returns the result of a componentwise modulus operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator % (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs); }

        /// <summary>Returns the result of a componentwise modulus operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator % (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3); }


        /// <summary>Returns the result of a componentwise increment operation on a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator ++ (uint3x4 val) { return new uint3x4 (++val.c0, ++val.c1, ++val.c2, ++val.c3); }


        /// <summary>Returns the result of a componentwise decrement operation on a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator -- (uint3x4 val) { return new uint3x4 (--val.c0, --val.c1, --val.c2, --val.c3); }


        /// <summary>Returns the result of a componentwise less than operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator < (uint3x4 lhs, uint3x4 rhs) { return new bool3x4 (lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3); }

        /// <summary>Returns the result of a componentwise less than operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator < (uint3x4 lhs, uint rhs) { return new bool3x4 (lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs); }

        /// <summary>Returns the result of a componentwise less than operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator < (uint lhs, uint3x4 rhs) { return new bool3x4 (lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3); }


        /// <summary>Returns the result of a componentwise less or equal operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator <= (uint3x4 lhs, uint3x4 rhs) { return new bool3x4 (lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3); }

        /// <summary>Returns the result of a componentwise less or equal operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator <= (uint3x4 lhs, uint rhs) { return new bool3x4 (lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs); }

        /// <summary>Returns the result of a componentwise less or equal operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator <= (uint lhs, uint3x4 rhs) { return new bool3x4 (lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3); }


        /// <summary>Returns the result of a componentwise greater than operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator > (uint3x4 lhs, uint3x4 rhs) { return new bool3x4 (lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3); }

        /// <summary>Returns the result of a componentwise greater than operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator > (uint3x4 lhs, uint rhs) { return new bool3x4 (lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs); }

        /// <summary>Returns the result of a componentwise greater than operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator > (uint lhs, uint3x4 rhs) { return new bool3x4 (lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3); }


        /// <summary>Returns the result of a componentwise greater or equal operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator >= (uint3x4 lhs, uint3x4 rhs) { return new bool3x4 (lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator >= (uint3x4 lhs, uint rhs) { return new bool3x4 (lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator >= (uint lhs, uint3x4 rhs) { return new bool3x4 (lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3); }


        /// <summary>Returns the result of a componentwise unary minus operation on a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator - (uint3x4 val) { return new uint3x4 (-val.c0, -val.c1, -val.c2, -val.c3); }


        /// <summary>Returns the result of a componentwise unary plus operation on a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator + (uint3x4 val) { return new uint3x4 (+val.c0, +val.c1, +val.c2, +val.c3); }


        /// <summary>Returns the result of a componentwise left shift operation on a uint3x4 matrix by a number of bits specified by a single int.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator << (uint3x4 x, int n) { return new uint3x4 (x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n); }

        /// <summary>Returns the result of a componentwise right shift operation on a uint3x4 matrix by a number of bits specified by a single int.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator >> (uint3x4 x, int n) { return new uint3x4 (x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n); }

        /// <summary>Returns the result of a componentwise equality operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator == (uint3x4 lhs, uint3x4 rhs) { return new bool3x4 (lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3); }

        /// <summary>Returns the result of a componentwise equality operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator == (uint3x4 lhs, uint rhs) { return new bool3x4 (lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs); }

        /// <summary>Returns the result of a componentwise equality operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator == (uint lhs, uint3x4 rhs) { return new bool3x4 (lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3); }


        /// <summary>Returns the result of a componentwise not equal operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator != (uint3x4 lhs, uint3x4 rhs) { return new bool3x4 (lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3); }

        /// <summary>Returns the result of a componentwise not equal operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator != (uint3x4 lhs, uint rhs) { return new bool3x4 (lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs); }

        /// <summary>Returns the result of a componentwise not equal operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3x4 operator != (uint lhs, uint3x4 rhs) { return new bool3x4 (lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3); }


        /// <summary>Returns the result of a componentwise bitwise not operation on a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator ~ (uint3x4 val) { return new uint3x4 (~val.c0, ~val.c1, ~val.c2, ~val.c3); }


        /// <summary>Returns the result of a componentwise bitwise and operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator & (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3); }

        /// <summary>Returns the result of a componentwise bitwise and operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator & (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs); }

        /// <summary>Returns the result of a componentwise bitwise and operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator & (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3); }


        /// <summary>Returns the result of a componentwise bitwise or operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator | (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3); }

        /// <summary>Returns the result of a componentwise bitwise or operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator | (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs); }

        /// <summary>Returns the result of a componentwise bitwise or operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator | (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3); }


        /// <summary>Returns the result of a componentwise bitwise exclusive or operation on two uint3x4 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator ^ (uint3x4 lhs, uint3x4 rhs) { return new uint3x4 (lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3); }

        /// <summary>Returns the result of a componentwise bitwise exclusive or operation on a uint3x4 matrix and a uint value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator ^ (uint3x4 lhs, uint rhs) { return new uint3x4 (lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs); }

        /// <summary>Returns the result of a componentwise bitwise exclusive or operation on a uint value and a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 operator ^ (uint lhs, uint3x4 rhs) { return new uint3x4 (lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3); }



        /// <summary>Returns the uint3 element at a specified index.</summary>
        unsafe public uint3 this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (uint3x4* array = &this) { return ((uint3*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (uint3* array = &c0) { array[index] = value; }
            }
        }

        /// <summary>Returns true if the uint3x4 is equal to a given uint3x4, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(uint3x4 rhs) { return c0.Equals(rhs.c0) && c1.Equals(rhs.c1) && c2.Equals(rhs.c2) && c3.Equals(rhs.c3); }

        /// <summary>Returns true if the uint3x4 is equal to a given uint3x4, false otherwise.</summary>
        public override bool Equals(object o) { return Equals((uint3x4)o); }


        /// <summary>Returns a hash code for the uint3x4.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)math.hash(this); }


        /// <summary>Returns a string representation of the uint3x4.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("uint3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", c0.x, c1.x, c2.x, c3.x, c0.y, c1.y, c2.y, c3.y, c0.z, c1.z, c2.z, c3.z);
        }

        /// <summary>Returns a string representation of the uint3x4 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("uint3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", c0.x.ToString(format, formatProvider), c1.x.ToString(format, formatProvider), c2.x.ToString(format, formatProvider), c3.x.ToString(format, formatProvider), c0.y.ToString(format, formatProvider), c1.y.ToString(format, formatProvider), c2.y.ToString(format, formatProvider), c3.y.ToString(format, formatProvider), c0.z.ToString(format, formatProvider), c1.z.ToString(format, formatProvider), c2.z.ToString(format, formatProvider), c3.z.ToString(format, formatProvider));
        }

    }

    public static partial class math
    {
        /// <summary>Returns a uint3x4 matrix constructed from four uint3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(uint3 c0, uint3 c1, uint3 c2, uint3 c3) { return new uint3x4(c0, c1, c2, c3); }

        /// <summary>Returns a uint3x4 matrix constructed from from 12 uint values given in row-major order.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(uint m00, uint m01, uint m02, uint m03,
                                      uint m10, uint m11, uint m12, uint m13,
                                      uint m20, uint m21, uint m22, uint m23)
        {
            return new uint3x4(m00, m01, m02, m03,
                               m10, m11, m12, m13,
                               m20, m21, m22, m23);
        }

        /// <summary>Returns a uint3x4 matrix constructed from a single uint value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(uint v) { return new uint3x4(v); }

        /// <summary>Returns a uint3x4 matrix constructed from a single bool value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(bool v) { return new uint3x4(v); }

        /// <summary>Return a uint3x4 matrix constructed from a bool3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(bool3x4 v) { return new uint3x4(v); }

        /// <summary>Returns a uint3x4 matrix constructed from a single int value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(int v) { return new uint3x4(v); }

        /// <summary>Return a uint3x4 matrix constructed from a int3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(int3x4 v) { return new uint3x4(v); }

        /// <summary>Returns a uint3x4 matrix constructed from a single float value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(float v) { return new uint3x4(v); }

        /// <summary>Return a uint3x4 matrix constructed from a float3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(float3x4 v) { return new uint3x4(v); }

        /// <summary>Returns a uint3x4 matrix constructed from a single double value by converting it to uint and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(double v) { return new uint3x4(v); }

        /// <summary>Return a uint3x4 matrix constructed from a double3x4 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3x4 uint3x4(double3x4 v) { return new uint3x4(v); }

        /// <summary>Return the uint4x3 transpose of a uint3x4 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4x3 transpose(uint3x4 v)
        {
            return uint4x3(
                v.c0.x, v.c0.y, v.c0.z,
                v.c1.x, v.c1.y, v.c1.z,
                v.c2.x, v.c2.y, v.c2.z,
                v.c3.x, v.c3.y, v.c3.z);
        }

        /// <summary>Returns a uint hash code of a uint3x4 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(uint3x4 v)
        {
            return csum(v.c0 * uint3(0xD1224537u, 0xE99ED6F3u, 0x48125549u) + 
                        v.c1 * uint3(0xEEE2123Bu, 0xE3AD9FE5u, 0xCE1CF8BFu) + 
                        v.c2 * uint3(0x7BE39F3Bu, 0xFAB9913Fu, 0xB4501269u) + 
                        v.c3 * uint3(0xE04B89FDu, 0xDB3DE101u, 0x7B6D1B4Bu)) + 0x58399E77u;
        }

        /// <summary>
        /// Returns a uint3 vector hash code of a uint3x4 vector.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 hashwide(uint3x4 v)
        {
            return (v.c0 * uint3(0x5EAC29C9u, 0xFC6014F9u, 0x6BF6693Fu) + 
                    v.c1 * uint3(0x9D1B1D9Bu, 0xF842F5C1u, 0xA47EC335u) + 
                    v.c2 * uint3(0xA477DF57u, 0xC4B1493Fu, 0xBA0966D3u) + 
                    v.c3 * uint3(0xAFBEE253u, 0x5B419C01u, 0x515D90F5u)) + 0xEC9F68F3u;
        }

    }
}
