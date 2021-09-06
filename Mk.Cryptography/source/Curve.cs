using System;
using System.Globalization;
using System.Numerics;

namespace Mk.Cryptography
{
    /// <summary>
    /// Represent elliptic curve: y ^ 2 = x ^ 3 + a * x + b (mod p).
    /// </summary>
    public class Curve
    {
        /// <summary>
        /// https://tools.ietf.org/id/draft-kasamatsu-bncurves-01.html#curve256
        /// </summary>
        /// <remarks>
        /// Curve-ID: Fp256BN
        /// p_b = 0xfffffffffffcf0cd46e5f25eee71a49f0cdc65fb12980a82d3292ddbaed33013
        /// A = 0
        /// B = 3
        /// x = 1
        /// y = 2
        /// q = 0xfffffffffffcf0cd46e5f25eee71a49e0cdc65fb1299921af62d536cd10b500d
        /// h = 1
        /// </remarks>
        public static Curve Fp256BN { get; } = new(
            0, 3,
            BigInteger.Parse("0fffffffffffcf0cd46e5f25eee71a49f0cdc65fb12980a82d3292ddbaed33013", NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber),
            1, 2);


        public BigInteger A { get; }
        public BigInteger B { get; }
        public BigInteger P { get; }

        public Point2 BasePoint { get; }

        public Curve(BigInteger a, BigInteger b, BigInteger p, BigInteger x, BigInteger y)
        {
            if (p <= 0)
                throw new ArgumentOutOfRangeException($"'{nameof(p)}' is negative or zero.");

            A = a;
            B = b;
            P = p;
            BasePoint = new Point2(x, y);
        }

        public bool Contains(Point2 point)
        {
            return (point.Y * point.Y) % P == 
                   (point.X * point.X * point.X + A * point.X + B) % P;
        }
    }
}