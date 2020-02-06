using Mocking.DataGenerator.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mocking.DataGenerator
{
    class ValueResolver
    {
        public Func<object, object> Resolver { get; set; }
    }

    public class MockDataGenerator<TModel>
    {
        private readonly Dictionary<LambdaExpression, Func<TModel, CultureInfo, object>> _expressions = new Dictionary<LambdaExpression, Func<TModel, CultureInfo, object>>();

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Func<TModel, TProperty> valueResolver)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new Func<TModel, CultureInfo, object>((model, cultureInfo) => valueResolver(model)));
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Func<TProperty> valueResolver)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new Func<TModel, CultureInfo, object>((model, cultureInfo) => valueResolver()));
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new Func<TModel, CultureInfo, object>((model, cultureInfo) => value));
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, IDataGenerator<TProperty> randomData)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new Func<TModel, CultureInfo, object>((model, cultureInfo) => randomData.Get(cultureInfo)));
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Expression<Func<DataGenerator<TProperty>, IDataGenerator<TProperty>>> factory)
        {
            if (!_expressions.ContainsKey(expression))
            {
                var dataGenerator = (DataGenerator<TProperty>)Activator.CreateInstance(typeof(DataGenerator<>).MakeGenericType(typeof(TProperty)));

                var randomData = factory.Compile()(dataGenerator);

                _expressions.Add(expression, new Func<TModel, CultureInfo, object>((model, cultureInfo) => randomData.Get(cultureInfo)));
            }

            return this;
        }

        public List<TModel> Generate(int count = 50)
        {
            var data = new List<TModel>();

            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                var cultureInfo = cultures[rnd.Next(0, cultures.Length)];

                var model = Activator.CreateInstance<TModel>();

                foreach (var expression in _expressions)
                {
                    var memberExpression = (MemberExpression)expression.Key.Body;

                    var pi = (PropertyInfo)memberExpression.Member;

                    var value = expression.Value(model, cultureInfo);

                    pi.SetValue(model, value, new object[] { });
                }

                data.Add(model);
            }

            return data;
        }

        public TModel GenerateOne()
        {
            var model = Activator.CreateInstance<TModel>();

            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var rnd = new Random();
            var cultureInfo = cultures[rnd.Next(0, cultures.Length)];

            foreach (var expression in _expressions)
            {
                var memberExpression = (MemberExpression)expression.Key.Body;

                var pi = (PropertyInfo)memberExpression.Member;

                pi.SetValue(model, expression.Value(model, cultureInfo), new object[] { });
            }

            return model;
        }
    }

    public class DataGenerator<TProperty>
    {

    }

    public static class DataGeneratorExtensions
    {
        public static IDataGenerator<TProperty> FromArray<TProperty>(this DataGenerator<TProperty> property, TProperty[] arr)
        {
            return new FromArray<TProperty>(arr);
        }

        #region Enum
        public static IDataGenerator<TProperty> FromEnum<TProperty>(this DataGenerator<TProperty> property) where TProperty : struct, Enum
        {
            return new FromEnum<TProperty>();
        }

        public static IDataGenerator<TProperty?> FromEnum<TProperty>(this DataGenerator<TProperty?> property) where TProperty : struct, Enum
        {
            return new NullableFromEnum<TProperty>();
        }
        #endregion

        public static IDataGenerator<string> IBAN(this DataGenerator<string> property)
        {
            return new IBANGenerator();
        }

        public static IDataGenerator<string> Name(this DataGenerator<string> property)
        {
            return new NameGenerator();
        }

        public static IDataGenerator<string> Surname(this DataGenerator<string> property)
        {
            return new SurnameGenerator();
        }

        #region Phone
        public static IDataGenerator<string> Phone(this DataGenerator<string> property)
        {
            return new PhoneGenerator(format: "+#(###)###-##-##");
        }

        public static IDataGenerator<string> Phone(this DataGenerator<string> property, string format)
        {
            return new PhoneGenerator(format);
        }
        #endregion

        #region Url
        public static IDataGenerator<string> Url(this DataGenerator<string> property)
        {
            return new URLGenerator(includePath: false);
        }

        public static IDataGenerator<string> Url(this DataGenerator<string> property, bool includePath)
        {
            return new URLGenerator(includePath);
        } 
        #endregion

        public static IDataGenerator<string> Email(this DataGenerator<string> property)
        {
            return new EmailGenerator();
        }

        public static IDataGenerator<string> CurrencySymbol(this DataGenerator<string> property)
        {
            return new CurrencySymbolGenerator();
        }

        public static IDataGenerator<string> CurrencyCode(this DataGenerator<string> property)
        {
            return new CurrencyCodeGenerator();
        }

        public static IDataGenerator<string> CreditCard(this DataGenerator<string> property)
        {
            return new CreditCardGenerator();
        }

        public static IDataGenerator<string> Region(this DataGenerator<string> property)
        {
            return new RegionGenerator();
        }

        #region Random String
        public static IDataGenerator<string> Random(this DataGenerator<string> property)
        {
            return new RandomStringGenerator();
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter)
        {
            return new RandomStringGenerator(upperLetter);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter, int lowerLetter)
        {
            return new RandomStringGenerator(upperLetter, lowerLetter);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter, int lowerLetter, int digit)
        {
            return new RandomStringGenerator(upperLetter, lowerLetter, digit);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter, int lowerLetter, int digit, int specialChars)
        {
            return new RandomStringGenerator(upperLetter, lowerLetter, digit, specialChars);
        }
        #endregion

        public static IDataGenerator<string> MD5(this DataGenerator<string> property)
        {
            return new MD5Generator();
        }

        #region Lorem Ipsum
        public static IDataGenerator<string> LoremIpsum(this DataGenerator<string> property)
        {
            return new LoremIpsumGenerator(sentenceCount: 3, paragraphCount: 1);
        }

        public static IDataGenerator<string> LoremIpsum(this DataGenerator<string> property, int sentenceCount)
        {
            return new LoremIpsumGenerator(sentenceCount, paragraphCount: 1);
        }

        public static IDataGenerator<string> LoremIpsum(this DataGenerator<string> property, int sentenceCount, int paragraphCount)
        {
            return new LoremIpsumGenerator(sentenceCount, paragraphCount);
        }
        #endregion

        public static IDataGenerator<string> Language(this DataGenerator<string> property)
        {
            return new LanguageGenerator();
        }

        public static IDataGenerator<string> IPV4(this DataGenerator<string> property)
        {
            return new IPV4Generator();
        }

        public static IDataGenerator<string> Gender(this DataGenerator<string> property)
        {
            return new GenderGenerator();
        }

        public static IDataGenerator<string> Culture(this DataGenerator<string> property)
        {
            return new CultureGenerator();
        }

        public static IDataGenerator<string> Country(this DataGenerator<string> property)
        {
            return new CountryGenerator();
        }

        public static IDataGenerator<bool> Random(this DataGenerator<bool> property)
        {
            return new RandomBoolean();
        }

        public static IDataGenerator<bool?> Random(this DataGenerator<bool?> property)
        {
            return new NullableRandomBoolean();
        }

        #region Random Integer
        public static IDataGenerator<int> Random(this DataGenerator<int> property)
        {
            return new IntegerGenerator(min: int.MinValue, max: int.MaxValue);
        }

        public static IDataGenerator<int> Random(this DataGenerator<int> property, int min)
        {
            return new IntegerGenerator(min, int.MaxValue);
        }

        public static IDataGenerator<int> Random(this DataGenerator<int> property, int min, int max)
        {
            return new IntegerGenerator(min, max);
        }

        public static IDataGenerator<int?> Random(this DataGenerator<int?> property)
        {
            return new NullableIntegerGenerator(min: int.MinValue, max: int.MaxValue);
        }

        public static IDataGenerator<int?> Random(this DataGenerator<int?> property, int min)
        {
            return new NullableIntegerGenerator(min, int.MaxValue);
        }

        public static IDataGenerator<int?> Random(this DataGenerator<int?> property, int min, int max)
        {
            return new NullableIntegerGenerator(min, max);
        }
        #endregion

        #region Random Byte
        public static IDataGenerator<byte> Random(this DataGenerator<byte> property)
        {
            return new ByteGenerator(min: byte.MinValue, max: byte.MaxValue);
        }

        public static IDataGenerator<byte> Random(this DataGenerator<byte> property, byte min)
        {
            return new ByteGenerator(min, byte.MaxValue);
        }

        public static IDataGenerator<byte> Random(this DataGenerator<byte> property, byte min, byte max)
        {
            return new ByteGenerator(min, max);
        }

        public static IDataGenerator<byte?> Random(this DataGenerator<byte?> property)
        {
            return new NullableByteGenerator(min: byte.MinValue, max: byte.MaxValue);
        }

        public static IDataGenerator<byte?> Random(this DataGenerator<byte?> property, byte min)
        {
            return new NullableByteGenerator(min, byte.MaxValue);
        }

        public static IDataGenerator<byte?> Random(this DataGenerator<byte?> property, byte min, byte max)
        {
            return new NullableByteGenerator(min, max);
        }
        #endregion

        #region Random Short
        public static IDataGenerator<short> Random(this DataGenerator<short> property)
        {
            return new ShortGenerator(min: short.MinValue, max: short.MaxValue);
        }

        public static IDataGenerator<short> Random(this DataGenerator<short> property, short min)
        {
            return new ShortGenerator(min, short.MaxValue);
        }

        public static IDataGenerator<short> Random(this DataGenerator<short> property, short min, short max)
        {
            return new ShortGenerator(min, max);
        }

        public static IDataGenerator<short?> Random(this DataGenerator<short?> property)
        {
            return new NullableShortGenerator(min: short.MinValue, max: short.MaxValue);
        }

        public static IDataGenerator<short?> Random(this DataGenerator<short?> property, short min)
        {
            return new NullableShortGenerator(min, short.MaxValue);
        }

        public static IDataGenerator<short?> Random(this DataGenerator<short?> property, short min, short max)
        {
            return new NullableShortGenerator(min, max);
        }
        #endregion

        #region Random Long
        public static IDataGenerator<long> Random(this DataGenerator<long> property)
        {
            return new LongGenerator(min: long.MinValue, max: long.MaxValue);
        }

        public static IDataGenerator<long> Random(this DataGenerator<long> property, long min)
        {
            return new LongGenerator(min, int.MaxValue);
        }

        public static IDataGenerator<long> Random(this DataGenerator<long> property, long min, long max)
        {
            return new LongGenerator(min, max);
        }

        public static IDataGenerator<long?> Random(this DataGenerator<long?> property)
        {
            return new NullableLongGenerator(min: long.MinValue, max: long.MaxValue);
        }

        public static IDataGenerator<long?> Random(this DataGenerator<long?> property, long min)
        {
            return new NullableLongGenerator(min, long.MaxValue);
        }

        public static IDataGenerator<long?> Random(this DataGenerator<long?> property, long min, long max)
        {
            return new NullableLongGenerator(min, max);
        }
        #endregion

        #region Money
        public static IDataGenerator<decimal> Random(this DataGenerator<decimal> property)
        {
            return new DecimalGenerator();
        }

        public static IDataGenerator<decimal?> Random(this DataGenerator<decimal?> property)
        {
            return new NullableDecimalGenerator();
        }
        #endregion

        #region AutoIncrement
        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property)
        {
            return new AutoIncrementDataGenerator();
        }

        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property, int start)
        {
            return new AutoIncrementDataGenerator(start, 1);
        }

        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property, int start, int increment)
        {
            return new AutoIncrementDataGenerator(start, increment);
        }

        public static IDataGenerator<int?> AutoIncrement(this DataGenerator<int?> property)
        {
            return new NullableAutoIncrementDataGenerator(int.MinValue, int.MaxValue);
        }

        public static IDataGenerator<int?> AutoIncrement(this DataGenerator<int?> property, int start)
        {
            return new NullableAutoIncrementDataGenerator(start, 1);
        }

        public static IDataGenerator<int?> AutoIncrement(this DataGenerator<int?> property, int start, int increment)
        {
            return new NullableAutoIncrementDataGenerator(start, increment);
        }
        #endregion

        #region Guid
        public static IDataGenerator<Guid> Guid(this DataGenerator<Guid> property)
        {
            return new GuidGenerator();
        }

        public static IDataGenerator<Guid?> Guid(this DataGenerator<Guid?> property)
        {
            return new NullableGuidGenerator();
        }

        public static IDataGenerator<string> Guid(this DataGenerator<string> property)
        {
            return new StringGuidGenerator();
        }
        #endregion

        #region DateTime
        public static IDataGenerator<DateTime> Random(this DataGenerator<DateTime> property)
        {
            return new DateTimeGenerator();
        }

        public static IDataGenerator<DateTime?> Random(this DataGenerator<DateTime?> property)
        {
            return new NullableDateTimeGenerator();
        }

        public static IDataGenerator<string> DateTime(this DataGenerator<string> property)
        {
            return new StringDateTimeGenerator(format: null);
        }

        public static IDataGenerator<string> DateTime(this DataGenerator<string> property, string format)
        {
            return new StringDateTimeGenerator(format);
        }
        #endregion

        public static IDataGenerator<TProperty> Object<TProperty>(this DataGenerator<TProperty> property, MockDataGenerator<TProperty> data) where TProperty : class, new()
        {
            return new ComplexObject<TProperty>(data);
        }

        #region List

        public static IDataGenerator<TProperty> List<TProperty>(this DataGenerator<TProperty> property) where TProperty : class, IList
        {
            return new PrimitiveListGenerator<TProperty>(count: 10);
        }

        public static IDataGenerator<TProperty> List<TProperty>(this DataGenerator<TProperty> property, int count) where TProperty : class, IList
        {
            return new PrimitiveListGenerator<TProperty>(count);
        }

        public static IDataGenerator<TProperty> List<TProperty, TElement>(this DataGenerator<TProperty> property, MockDataGenerator<TElement> mocker) where TProperty : class, IList
        {
            return new ListGenerator<TProperty, TElement>(mocker, count: 10);
        }

        public static IDataGenerator<TProperty> List<TProperty, TElement>(this DataGenerator<TProperty> property, MockDataGenerator<TElement> mocker, int count) where TProperty : class, IList
        {
            return new ListGenerator<TProperty, TElement>(mocker, count);
        }
        #endregion

        #region Array
        public static IDataGenerator<TProperty> Array<TProperty>(this DataGenerator<TProperty> property) where TProperty : class, IEnumerable
        {
            return new PrimitiveArrayGenerator<TProperty>(count: 10);
        }

        public static IDataGenerator<TProperty> Array<TProperty>(this DataGenerator<TProperty> property, int count) where TProperty : class, IEnumerable
        {
            return new PrimitiveArrayGenerator<TProperty>(count);
        }

        public static IDataGenerator<TProperty> Array<TProperty, TElementType>(this DataGenerator<TProperty> property, MockDataGenerator<TElementType> mocker) where TProperty : class, IEnumerable
        {
            return new ArrayGenerator<TProperty, TElementType>(mocker, count: 10);
        }

        public static IDataGenerator<TProperty> Array<TProperty, TElementType>(this DataGenerator<TProperty> property, MockDataGenerator<TElementType> mocker, int count) where TProperty : class, IEnumerable
        {
            return new ArrayGenerator<TProperty, TElementType>(mocker, count);
        }
        #endregion
    }
}
