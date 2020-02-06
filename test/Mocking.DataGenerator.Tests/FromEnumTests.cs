using Mocking.DataGenerator.Generators;
using System.Globalization;
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

            TestEnum value1 = fromEnum.Get(new CultureInfo("en-US"));
            TestEnum value2 = fromEnum.Get(new CultureInfo("en-US"));
            TestEnum value3 = fromEnum.Get(new CultureInfo("en-US"));
            TestEnum value4 = fromEnum.Get(new CultureInfo("en-US"));

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