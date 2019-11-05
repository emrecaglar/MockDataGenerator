using Mocking.DataGenerator.Generators;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class FromArrayTests
    {
        [Fact]
        public void FromArray_ShoulBeContains_Arr()
        {
            var arr = new[] { "Test1", "Test2", "Test3" };

            var fromArray = new FromArray<string>(arr);

            string value1= fromArray.Get();
            string value2 = fromArray.Get();
            string value3 = fromArray.Get();

            Assert.Contains(value1, arr);
            Assert.Contains(value2, arr);
            Assert.Contains(value3, arr);
        }
    }
}
