// Neural v3.0 — Matrix3x3.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.AI.Math
{
    public struct Matrix3x3
    {
        public float M11, M12, M13;
        public float M21, M22, M23;
        public float M31, M32, M33;

        public static Matrix3x3 Identity => new Matrix3x3
        {
            M11 = 1,
            M22 = 1,
            M33 = 1
        };

        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3
            {
                M11 = a.M11 + b.M11,
                M12 = a.M12 + b.M12,
                M13 = a.M13 + b.M13,
                M21 = a.M21 + b.M21,
                M22 = a.M22 + b.M22,
                M23 = a.M23 + b.M23,
                M31 = a.M31 + b.M31,
                M32 = a.M32 + b.M32,
                M33 = a.M33 + b.M33,
            };
        }

        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3
            {
                M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
                M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
                M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,

                M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
                M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
                M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,

                M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
                M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
                M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33,
            };
        }

        public static Matrix3x3 operator *(Matrix3x3 m, float scalar)
        {
            return new Matrix3x3
            {
                M11 = m.M11 * scalar,
                M12 = m.M12 * scalar,
                M13 = m.M13 * scalar,
                M21 = m.M21 * scalar,
                M22 = m.M22 * scalar,
                M23 = m.M23 * scalar,
                M31 = m.M31 * scalar,
                M32 = m.M32 * scalar,
                M33 = m.M33 * scalar,
            };
        }

        public static Matrix3x3 operator /(Matrix3x3 m, float scalar)
        {
            float inv = 1.0f / scalar;
            return m * inv;
        }

        public static Vector3 operator *(Matrix3x3 m, Vector3 v)
        {
            return new Vector3(
                m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z,
                m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z,
                m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z
            );
        }

        public override string ToString()
        {
            return $"[{M11}, {M12}, {M13}]\n[{M21}, {M22}, {M23}]\n[{M31}, {M32}, {M33}]";
        }
    }
}
