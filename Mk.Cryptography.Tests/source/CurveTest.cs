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
    }
}