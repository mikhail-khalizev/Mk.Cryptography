using Xunit.Abstractions;

namespace Mk.ReverseEngineering.Tests
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