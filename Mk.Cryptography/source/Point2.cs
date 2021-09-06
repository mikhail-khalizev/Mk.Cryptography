using System.Numerics;

namespace Mk.Cryptography
{
    public struct Point2
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
        
        public Point2(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }
    }
}