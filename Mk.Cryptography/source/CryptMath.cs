using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace Mk.Cryptography
{
    public class CryptMath
    {
        /// <summary>
        /// Returns 'y', where 'y = 1 / x (mod n)'. I.e. 'x * y (mod n) = 1'.
        /// 'n' should be primary.
        /// </summary>
        public static BigInteger Invert(BigInteger x, BigInteger n)
        {
            // Extended Euclidean Algorithm. It's the 'division' in elliptic curves.

            x = PositiveModulo(x, n);

            var a = n;
            var b = x % n;

            if (b.IsZero)
                return 0;

            // Part 1.
            // GCD (https://en.wikipedia.org/wiki/Euclidean_algorithm):
            //
            // a = b * q0 + r1                    and GCD(a, b) = GCD(b, r1)
            //                                   
            // b = r1 * q1 + r2                   and GCD(a, b) = GCD(r1, r2)
            // r1 = r2 * q2 + r3                  and GCD(a, b) = GCD(r2, r3)
            // ...
            // r(k-2) = r(k-1) * q(k-1) + r(k)    and GCD(a, b) = GCD(r(k-1),r(k))
            // r(k-1) = r(k)   * q(k)   + r(k+1)      and GCD(a, b) = GCD(r(k),r(k+1))
            // ...
            // r(n-1) = r(n) * q(n)               and GCD(a, b) = GCD(r(n-1),r(n)) = r(n)

            // Part 2.
            // GCD(a, b) = a * ac + b * bc
            // and GCD(a, b) = 1 because in our case when 'a' is primary.
            //
            // 1 = a * ac + b * bc
            // 1 = a * ac + b * bc (mod a)
            // 1 = b * bc (mod a);
            //   so 'bc' is what we should find.

            // Part 3.
            // How to find 'bc'.
            // r1 = a * 1 + b * (-q0)
            // ...
            // r(k-1) = a * ac(k-1) + b * bc(k-1)
            // r(k)   = a * ac(k)   + b * bc(k)
            // r(k+1) = a * ac(k+1) + b * bc(k+1)
            //        = r(k-1) - r(k) * q(k)
            //        = a * ac(k-1) + b * bc(k-1) - (a * ac(k) + b * bc(k)) * q(k) 
            //        = a * ac(k-1) + b * bc(k-1) - a * ac(k) * q(k) - b * bc(k) * q(k)
            //        = a * (ac(k-1) - ac(k) * q(k)) + b * (bc(k-1) - bc(k) * q(k))
            //
            // So  ac(k+1) = ac(k-1) - ac(k) * q(k)
            //     bc(k+1) = bc(k-1) - bc(k) * q(k)     
            // 
            //    bc1 = -q0
            //    bc2 = 1 + q0 * q1
            // => bc0 = 1
            // => bc(-1) = 0 

            var bcPrev = BigInteger.Zero;
            var bc = BigInteger.One;

            while (1 < b)
            {
                var q = a / b;
                var r = a - (b * q); // a % b;

                var bcNew = bcPrev - bc * q;

                a = b;
                b = r;

                bcPrev = bc;
                bc = bcNew;
            }

            bc = PositiveModulo(bc, n);

#if DEBUG || TEST

            var mul = (bc * x) % n;
            Trace.Assert(mul == 0 || mul == 1);

#endif

            return bc;
        }

        public static BigInteger PositiveModulo(BigInteger dividend, BigInteger divisor)
        {
            var remainder = dividend % divisor;

            if (remainder < 0)
                remainder += divisor;

            return remainder;
        }

        public static BigInteger BigRandom(BigInteger fromInclusive, BigInteger toExclusive)
        {
            if (toExclusive <= fromInclusive)
                throw new ArgumentException($"{nameof(toExclusive)} <= {nameof(fromInclusive)}");

            var range = toExclusive - fromInclusive;
            
            var bytes = new byte[range.GetByteCount(true)];
            RandomNumberGenerator.Fill(bytes);
            var rndBig = new BigInteger(bytes, true) % range;

            var result = fromInclusive + rndBig;

#if DEBUG || TEST
            Trace.Assert(fromInclusive <= result && result < toExclusive);
#endif

            return result;
        }
    }
}
