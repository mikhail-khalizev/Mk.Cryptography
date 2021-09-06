using System.Numerics;

namespace Mk.Cryptography
{
    public class EcPublicKey
    {
        public EcCurve Curve { get; }
        public EcPoint Point { get; }

        public EcPublicKey(EcCurve curve, EcPoint point)
        {
            Curve = curve;
            Point = point;
        }

        public bool Verify(BigInteger message, EcSignature signature)
        {
            var inv = CryptMath.Invert(signature.S, Curve.N);

            var u1 = Curve.Multiply(
                Curve.G,
                CryptMath.PositiveModulo((message * inv), Curve.N));
            
            var u2 = Curve.Multiply(
                Point,
                CryptMath.PositiveModulo(signature.R * inv, Curve.N));

            var add = Curve.Add(u1, u2);

            return signature.R == add.X;
        }
    }
}