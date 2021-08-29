using Xunit.Abstractions;

namespace Mk.Cryptography.Tests
{
    public class BaseTest
    {
        public ITestOutputHelper OutputHelper { get; }
        
        public BaseTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }
    }
}