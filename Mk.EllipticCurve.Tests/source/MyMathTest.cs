using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Mk.EllipticCurve.Tests
{
    public class MyMathTest
    {
        [Theory]
        [InlineData(0, 0, 11)]
        [InlineData(1, 1, 11)]
        [InlineData(2, 6, 11)]
        [InlineData(3, 4, 11)]
        [InlineData(4, 3, 11)]
        [InlineData(5, 9, 11)]
        [InlineData(6, 2, 11)]
        [InlineData(7, 8, 11)]
        [InlineData(8, 7, 11)]
        [InlineData(9, 5, 11)]
        [InlineData(10, 10, 11)]
        [InlineData(11, 0, 11)]
        [InlineData(12, 1, 11)]
        public void CheckInvert(int x, int y, int n)
        {
            var yy = MyMath.Invert(x, n);
            var xx = MyMath.Invert(y, n);

            yy.Should().Be(y % n);
            xx.Should().Be(x % n);
        }
    }
}
