using FluentAssertions;
using Xunit;

namespace Mk.Cryptography.Tests
{
    public class CurveTest
    {
        [Fact]
        public void CheckCurveContainsBasePoint()
        {
            var c = Curve.Fp256BN;
            c.Contains(c.BasePoint).Should().BeTrue();
        }

        [Theory]
        [InlineData(17, 10, 95, 31, 1, 54)]
        [InlineData(17, 10, 20, 34, 27, 7)]
        [InlineData(17, 10, 17, 87, 0, 0)]
        public void CheckAdd(int ax, int ay, int bx, int by, int cx, int cy)
        {
            // https://cdn.rawgit.com/andreacorbellini/ecc/920b29a/interactive/modk-add.html

            var curve = new Curve(2, 3, 1, 1, 97);

            var c = curve.Add(
                new EcPoint(ax, ay),
                new EcPoint(bx, by));

            c.X.Should().Be(cx);
            c.Y.Should().Be(cy);
        }
    }
}