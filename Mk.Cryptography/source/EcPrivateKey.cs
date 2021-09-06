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

        // MessageHash usually is sha256 result of real message.
        public EcSignature Sign(BigInteger messageHash)
        {
            BigInteger r;
            BigInteger s;

            do
            {
                BigInteger k;

                do
                {
                    k = CryptMath.BigRandom(1, Curve.N);
                    var p = Curve.Multiply(Curve.G, k);

                    r = CryptMath.PositiveModulo(p.X, Curve.N);
                } while (r == 0);

                s = CryptMath.PositiveModulo((messageHash + r * Secret) * CryptMath.Invert(k, Curve.N), Curve.N);
            } while (s == 0);

            return new EcSignature(r, s);
        }
    }
}