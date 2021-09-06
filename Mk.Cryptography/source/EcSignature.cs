using System.Numerics;

namespace Mk.Cryptography
{
    public class EcSignature
    {
        public BigInteger R { get; }
        public BigInteger S { get; }

        public EcSignature(BigInteger r, BigInteger s)
        {
            R = r;
            S = s;
        }
    }
}