using System.Numerics;
using FluentAssertions;
using Xunit;

namespace Mk.Cryptography.Tests
{
    public class EcdsaTest
    {
        [Fact]
        public void SignVerify()
        {
            var curve = EcCurve.Fp256BN;

            var privateKey = new EcPrivateKey(curve);
            var publicKey = privateKey.GetPublicKey();

            var message = CryptMath.BigRandom(0, BigInteger.Pow(2, 256));
            var signature = privateKey.Sign(message);

            publicKey.Verify(message, signature).Should().Be(true);
            publicKey.Verify(message, new EcSignature(signature.R, signature.S + 1)).Should().Be(false);
            publicKey.Verify(message, new EcSignature(signature.R + 1, signature.S)).Should().Be(false);
        }
    }
}