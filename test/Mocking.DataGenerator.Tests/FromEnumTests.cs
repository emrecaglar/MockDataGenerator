using Mocking.DataGenerator.Generators;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class FromEnumTests
    {


        [Fact]
        public void FromEnum_ShoulBeContains_Enum()
        {
            var gen = new MockDataGenerator<Foo>()
                .Register(x => x.NonNullableEnum, x => x.FromEnum<TestEnum>())
                .Register(x => x.NullableEnum, x => x.FromEnum<TestEnum>());

            var fromEnum = new FromEnum<TestEnum>();

            var enums = new[] { TestEnum.Bar, TestEnum.Car, TestEnum.Foo, TestEnum.Test };

            TestEnum value1 = fromEnum.Get();
            TestEnum value2 = fromEnum.Get();
            TestEnum value3 = fromEnum.Get();
            TestEnum value4 = fromEnum.Get();

            Assert.Contains(value1, enums);
            Assert.Contains(value2, enums);
            Assert.Contains(value3, enums);
            Assert.Contains(value4, enums);
        }
    }

    enum TestEnum
    {
        Foo,
        Bar,
        Car,
        Test
    }

    class Foo
    {
        public TestEnum NonNullableEnum { get; set; }

        public TestEnum? NullableEnum { get; set; }
    }
}