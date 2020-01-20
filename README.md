# MockDataGenerator
Generate mock data for POCO

[![NuGet](https://img.shields.io/nuget/v/MockDataGenerator.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/MockDataGenerator/) ![Nuget](https://img.shields.io/nuget/dt/mockdatagenerator) ![AppVeyor](https://img.shields.io/appveyor/ci/EmreALAR/mockdatagenerator) ![AppVeyor tests](https://img.shields.io/appveyor/tests/EmreALAR/mockdatagenerator)

```csharp

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            var data = new MockDataGenerator<Model>();

            data.Register(x => x.Id, x => x.Guid());
            data.Register(x => x.Name, x => x.Name());
            data.Register(x => x.Active, x => x.Random());
            data.Register(x => x.Level, x => x.Random());
            data.Register(x => x.IBAN, x => x.IBAN());
            data.Register(x => x.Checksum, x => x.MD5());
            data.Register(x => x.InsertedDate, x => x.Random());
            data.Register(x => x.Explanation, x => x.LoremIpsum());
            data.Register(x => x.AllowedIp, x => x.IPV4());
            data.Register(x => x.SecureType, x => x.FromEnum());
            data.Register(x => x.SomeField, "Const value");

            data.Register(x => x.Provider, x => x.ComplexObject(
                new MockDataGenerator<AnotherModel>()
                        .Register(s => s.Name, s=>s.Random(5))
                        .Register(s => s.Id, s=>s.Guid())));

            var mockData = data.Generate(count: 10);
        }
    }

    public enum MyEnum
    {
        Foo,
        Bar
    }

    public class Model
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int Level { get; set; }

        public string IBAN { get; set; }

        public string Checksum { get; set; }

        public DateTime InsertedDate { get; set; }

        public AnotherModel Provider { get; set; }

        public string Explanation { get; set; }

        public string AllowedIp { get; set; }

        public MyEnum SecureType { get; set; }

        public string SomeField { get; set; }
    }

    public class AnotherModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

```
