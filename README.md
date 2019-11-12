# MockDataGenerator
Generate mock data for POCO

[![NuGet](https://img.shields.io/nuget/v/MockDataGenerator.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/MockDataGenerator/) ![AppVeyor](https://img.shields.io/appveyor/ci/EmreALAR/mockdatagenerator) ![AppVeyor tests](https://img.shields.io/appveyor/tests/EmreALAR/mockdatagenerator)

```csharp

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            var data = new MockDataGenerator<MyType>();

            data.Register(x => x.Id, new GuidGenerator());
            data.Register(x => x.Name, new FromArray<string>(new[] { "TestString1", "TestString2" }));
            data.Register(x => x.Active, new RandomBoolean());
            data.Register(x => x.Level, new IntegerGenerator(0, 20));
            data.Register(x => x.IBAN, new IBANGenerator());
            data.Register(x => x.Checksum, new MD5Generator());
            data.Register(x => x.InsertedDate, new DateTimeGenerator());
            data.Register(x => x.Explanation, new LoremIpsumGenerator(5, 3));
            data.Register(x => x.AllowedIp, new IPV4Generator());
            data.Register(x => x.SecureType, new FromEnum<MyEnum>());
            data.Register(x => x.SomeField, "Const value");

            data.Register(x => x.Provider, new ComplexObject<AnotherType>(
                                                        new MockDataGenerator<AnotherType>()
                                                            .Register(x => x.Name, new RandomString(5))
                                                            .Register(x => x.Id, new GuidGenerator())));

            var mockData = data.Generate(count: 10);
        }
    }
    
    public enum MyEnum
    {
        Foo,
        Bar
    }

    public class MyType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int Level { get; set; }

        public string IBAN { get; set; }

        public string Checksum { get; set; }

        public DateTime InsertedDate { get; set; }
        
        public AnotherType Provider { get; set; }

        public string Explanation { get; set; }

        public string AllowedIp { get; set; }
        
        public MyEnum SecureType { get; set; }

        public string SomeField { get; set; }
    }

    public class AnotherType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

```
