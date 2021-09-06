using System.Numerics;

namespace Mk.Cryptography
{
    public class EcPrivateKey
    {
        public EcCurve Curve { get; }
        public BigInteger Secret { get; }

        public EcPrivateKey(EcCurve curve)
        {
            Curve = curve;
            Secret = CryptMath.BigRandom(1, curve.N);
        }

        public EcPrivateKey(EcCurve curve, BigInteger secret)
        {
            Curve = curve;
            Secret = secret;
        }

        public EcPublicKey GetPublicKey()
        {
            return new (
                Curve,
                Curve.Multiply(Curve.G, Secret));
        }

        // Message usually is sha256 result of real message.
        public EcSignature Sign(BigInteger message)
        {
            var randNum = CryptMath.BigRandom(1, Curve.N);
            var randSignPoint = Curve.Multiply(Curve.G, randNum);

            var r = CryptMath.PositiveModulo(randSignPoint.X, Curve.N);
            var s = CryptMath.PositiveModulo((message + r * Secret) * CryptMath.Invert(randNum, Curve.N), Curve.N);

            return new EcSignature(r, s);
        }
    }
}