// GENERATED CODE
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(half3.DebuggerProxy))]
    public partial struct half3 : System.IEquatable<half3>, IFormattable
    {
        public half x;
        public half y;
        public half z;

        /// <summary>half3 zero value.</summary>
        public static readonly half3 zero;

        /// <summary>Constructs a half3 vector from three half values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(half x, half y, half z)
        { 
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>Constructs a half3 vector from a half value and a half2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(half x, half2 yz)
        { 
            this.x = x;
            this.y = yz.x;
            this.z = yz.y;
        }

        /// <summary>Constructs a half3 vector from a half2 vector and a half value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(half2 xy, half z)
        { 
            this.x = xy.x;
            this.y = xy.y;
            this.z = z;
        }

        /// <summary>Constructs a half3 vector from a half3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(half3 xyz)
        { 
            this.x = xyz.x;
            this.y = xyz.y;
            this.z = xyz.z;
        }

        /// <summary>Constructs a half3 vector from a single half value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(half v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        /// <summary>Constructs a half3 vector from a single float value by converting it to half and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(float v)
        {
            this.x = (half)v;
            this.y = (half)v;
            this.z = (half)v;
        }

        /// <summary>Constructs a half3 vector from a float3 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(float3 v)
        {
            this.x = (half)v.x;
            this.y = (half)v.y;
            this.z = (half)v.z;
        }

        /// <summary>Constructs a half3 vector from a single double value by converting it to half and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(double v)
        {
            this.x = (half)v;
            this.y = (half)v;
            this.z = (half)v;
        }

        /// <summary>Constructs a half3 vector from a double3 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public half3(double3 v)
        {
            this.x = (half)v.x;
            this.y = (half)v.y;
            this.z = (half)v.z;
        }


        /// <summary>Implicitly converts a single half value to a half3 vector by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator half3(half v) { return new half3(v); }

        /// <summary>Explicitly converts a single float value to a half3 vector by converting it to half and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator half3(float v) { return new half3(v); }

        /// <summary>Explicitly converts a float3 vector to a half3 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator half3(float3 v) { return new half3(v); }

        /// <summary>Explicitly converts a single double value to a half3 vector by converting it to half and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator half3(double v) { return new half3(v); }

        /// <summary>Explicitly converts a double3 vector to a half3 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator half3(double3 v) { return new half3(v); }


        /// <summary>Returns the result of a componentwise equality operation on two half3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator == (half3 lhs, half3 rhs) { return new bool3 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z); }

        /// <summary>Returns the result of a componentwise equality operation on a half3 vector and a half value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator == (half3 lhs, half rhs) { return new bool3 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs); }

        /// <summary>Returns the result of a componentwise equality operation on a half value and a half3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator == (half lhs, half3 rhs) { return new bool3 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z); }


        /// <summary>Returns the result of a componentwise not equal operation on two half3 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator != (half3 lhs, half3 rhs) { return new bool3 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z); }

        /// <summary>Returns the result of a componentwise not equal operation on a half3 vector and a half value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator != (half3 lhs, half rhs) { return new bool3 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs); }

        /// <summary>Returns the result of a componentwise not equal operation on a half value and a half3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator != (half lhs, half3 rhs) { return new bool3 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z); }




        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 xzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(x, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 yzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(y, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half4 zzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half4(z, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, y, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, z, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 xzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, x, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, z, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 yzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half3 zzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half3(z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 xx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 xy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 xz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(x, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 yx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 yy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 yz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(y, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 zx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(z, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 zy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(z, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public half2 zz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new half2(z, z); }
        }



        /// <summary>Returns the half element at a specified index.</summary>
        unsafe public half this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (half3* array = &this) { return ((half*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (half* array = &x) { array[index] = value; }
            }
        }

        /// <summary>Returns true if the half3 is equal to a given half3, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(half3 rhs) { return x == rhs.x && y == rhs.y && z == rhs.z; }

        /// <summary>Returns true if the half3 is equal to a given half3, false otherwise.</summary>
        public override bool Equals(object o) { return Equals((half3)o); }


        /// <summary>Returns a hash code for the half3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)math.hash(this); }


        /// <summary>Returns a string representation of the half3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("half3({0}, {1}, {2})", x, y, z);
        }

        /// <summary>Returns a string representation of the half3 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("half3({0}, {1}, {2})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider));
        }

        internal sealed class DebuggerProxy
        {
            public half x;
            public half y;
            public half z;
            public DebuggerProxy(half3 v)
            {
                x = v.x;
                y = v.y;
                z = v.z;
            }
        }

    }

    public static partial class math
    {
        /// <summary>Returns a half3 vector constructed from three half values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(half x, half y, half z) { return new half3(x, y, z); }

        /// <summary>Returns a half3 vector constructed from a half value and a half2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(half x, half2 yz) { return new half3(x, yz); }

        /// <summary>Returns a half3 vector constructed from a half2 vector and a half value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(half2 xy, half z) { return new half3(xy, z); }

        /// <summary>Returns a half3 vector constructed from a half3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(half3 xyz) { return new half3(xyz); }

        /// <summary>Returns a half3 vector constructed from a single half value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(half v) { return new half3(v); }

        /// <summary>Returns a half3 vector constructed from a single float value by converting it to half and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(float v) { return new half3(v); }

        /// <summary>Return a half3 vector constructed from a float3 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(float3 v) { return new half3(v); }

        /// <summary>Returns a half3 vector constructed from a single double value by converting it to half and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(double v) { return new half3(v); }

        /// <summary>Return a half3 vector constructed from a double3 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static half3 half3(double3 v) { return new half3(v); }

        /// <summary>Returns a uint hash code of a half3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(half3 v)
        {
            return csum(uint3(v.x.value, v.y.value, v.z.value) * uint3(0x685835CFu, 0xC3D32AE1u, 0xB966942Fu)) + 0xFE9856B3u;
        }

        /// <summary>
        /// Returns a uint3 vector hash code of a half3 vector.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 hashwide(half3 v)
        {
            return (uint3(v.x.value, v.y.value, v.z.value) * uint3(0xFA3A3285u, 0xAD55999Du, 0xDCDD5341u)) + 0x94DDD769u;
        }

    }
}
