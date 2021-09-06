using System.Numerics;

namespace Mk.Cryptography
{
    public struct EcPoint
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
        
        public EcPoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }

        public bool IsZero => Y.IsZero;
    }
}