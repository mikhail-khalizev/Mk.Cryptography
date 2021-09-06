using FluentAssertions;
using Xunit;

namespace Mk.Cryptography.Tests
{
    public class EcCurveTest
    {
        [Fact]
        public void CheckCurveContainsBasePoint()
        {
            var c = EcCurve.Fp256BN;
            c.Contains(c.G).Should().BeTrue();
        }

        [Theory]
        [InlineData(17, 10, 95, 31, 1, 54)]
        [InlineData(17, 10, 20, 34, 27, 7)]
        [InlineData(17, 10, 17, 87, 0, 0)]
        public void CheckAdd(int ax, int ay, int bx, int by, int cx, int cy)
        {
            // https://cdn.rawgit.com/andreacorbellini/ecc/920b29a/interactive/modk-add.html

            var curve = new EcCurve(2, 3, 0, 0, 97, 0);

            var c = curve.Add(
                new EcPoint(ax, ay),
                new EcPoint(bx, by));

            c.X.Should().Be(cx);
            c.Y.Should().Be(cy);
        }

        [Theory]
        [InlineData(3, 6, 0, 0, 0)]
        [InlineData(3, 6, 1, 3, 6)]
        [InlineData(3, 6, 2, 80, 10)]
        [InlineData(3, 6, 3, 80, 87)]
        [InlineData(3, 6, 4, 3, 91)]
        [InlineData(3, 6, 5, 0, 0)]
        public void CheckMultiply(int ax, int ay, int n, int cx, int cy)
        {
            // https://cdn.rawgit.com/andreacorbellini/ecc/920b29a/interactive/modk-mul.html

            var curve = new EcCurve(2, 3, 0, 0, 97, 0);

            var c = curve.Multiply(
                new EcPoint(ax, ay),
                n);

            c.X.Should().Be(cx);
            c.Y.Should().Be(cy);
        }

        [Fact]
        public void CheckQofFp256BN()
        {
            var c = EcCurve.Fp256BN;

            var p = c.Multiply(c.G, c.N);
            p.IsZero.Should().BeTrue();
        }
    }
}