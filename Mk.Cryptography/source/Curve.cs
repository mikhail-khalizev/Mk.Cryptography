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
            0, 3, 1, 2,
            "fffffffffffcf0cd46e5f25eee71a49f0cdc65fb12980a82d3292ddbaed33013");


        public BigInteger A { get; }
        public BigInteger B { get; }
        public BigInteger P { get; }

        public EcPoint BasePoint { get; }

        public Curve(BigInteger a, BigInteger b, BigInteger x, BigInteger y, string pHex)
            : this(a, b, x, y, BigInteger.Parse('0' + pHex, NumberStyles.HexNumber))
        { }

        public Curve(BigInteger a, BigInteger b, BigInteger x, BigInteger y, BigInteger p)
        {
            if (p <= 0)
                throw new ArgumentOutOfRangeException($"'{nameof(p)}' is negative or zero.");

            A = a;
            B = b;
            P = p;
            BasePoint = new EcPoint(x, y);
        }

        public bool Contains(EcPoint point)
        {
            return BigInteger.Pow(point.Y, 2) % P == 
                   (BigInteger.Pow(point.X, 3) + A * point.X + B) % P;
        }

        public EcPoint Add(EcPoint a, EcPoint b)
        {
            // a + b + c = 0.

            if (a.IsZero)
                return b;
            if (b.IsZero)
                return a;

            BigInteger m;
            if (a.X == b.X)
            {
                if (a.Y == b.Y)
                {
                    m = (3 * BigInteger.Pow(a.X, 2) + A) * CryptMath.Invert(2 * a.Y, P) % P;
                }
                else
                {
                    return new EcPoint();
                }
            }
            else
            {
                m = CryptMath.PositiveModulo((a.Y - b.Y) * CryptMath.Invert(a.X - b.X, P), P);
            }

            var x = CryptMath.PositiveModulo(BigInteger.Pow(m, 2) - a.X - b.X, P);
            var y = CryptMath.PositiveModulo(-(a.Y + m * (x - a.X)), P);

            return new EcPoint(x, y);
        }
    }
}