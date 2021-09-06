using FluentAssertions;
using Xunit;

namespace Mk.Cryptography.Tests
{
    public class CryptMathTest
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
            var yy = CryptMath.Invert(x, n);
            yy.Should().Be(y % n);
        }

        [Theory]
        [InlineData(0, 0, 10)]  // zero
        [InlineData(1, 1, 10)]  
        [InlineData(2, 5, 10)]  // zero
        [InlineData(3, 7, 10)]  
        [InlineData(4, 5, 10)]  // zero
        [InlineData(5, 8, 10)]  // zero
        [InlineData(6, 5, 10)]  // zero
        [InlineData(7, 3, 10)]  
        [InlineData(8, 5, 10)]  // zero
        [InlineData(9, 9, 10)]
        [InlineData(10, 0, 10)] // zero
        [InlineData(11, 1, 10)]
        public void CheckInvertNotPrimary(int x, int y, int n)
        {
            var yy = CryptMath.Invert(x, n);
            yy.Should().Be(y % n);
        }
    }
}
