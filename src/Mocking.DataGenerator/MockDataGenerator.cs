using Mocking.DataGenerator.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Mocking.DataGenerator
{
    class ValueResolver
    {
        public Func<object> Resolver { get; set; }
    }

    public class MockDataGenerator<TModel>
    {
        private readonly Dictionary<LambdaExpression, ValueResolver> _expressions = new Dictionary<LambdaExpression, ValueResolver>();

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Func<TProperty> valueResolver)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => valueResolver() });
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => value });
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, IDataGenerator<TProperty> randomData)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => randomData.Get() });
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Expression<Func<DataGenerator<TProperty>, IDataGenerator<TProperty>>> factory)
        {
            if (!_expressions.ContainsKey(expression))
            {
                var dataGenerator = (DataGenerator<TProperty>)Activator.CreateInstance(typeof(DataGenerator<>).MakeGenericType(typeof(TProperty)));

                var randomData = factory.Compile()(dataGenerator);

                _expressions.Add(expression, new ValueResolver { Resolver = () => randomData.Get() });
            }

            return this;
        }

        public List<TModel> Generate(int count = 50)
        {
            var data = new List<TModel>();

            for (int i = 0; i < count; i++)
            {
                var model = Activator.CreateInstance<TModel>();

                foreach (var expression in _expressions)
                {
                    var memberExpression = (MemberExpression)expression.Key.Body;

                    var pi = (PropertyInfo)memberExpression.Member;

                    pi.SetValue(model, GetValue(expression), new object[] { });
                }

                data.Add(model);
            }

            return data;
        }

        private object GetValue(KeyValuePair<LambdaExpression, ValueResolver> dic)
        {
            return dic.Value.Resolver();
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

        public static IDataGenerator<TProperty> FromEnum<TProperty>(this DataGenerator<TProperty> property) where TProperty : Enum
        {
            return new FromEnum<TProperty>();
        }

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

        public static IDataGenerator<string> Url(this DataGenerator<string> property, bool includePath = true)
        {
            return new URLGenerator(includePath);
        }

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
            return new RandomString(upperLetter: 5, lowerLetter: 5, digit: 5, specialChars: 5);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter)
        {
            return new RandomString(upperLetter, lowerLetter: 5, digit: 5, specialChars: 5);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter, int lowerLetter)
        {
            return new RandomString(upperLetter, lowerLetter, digit: 5, specialChars: 5);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter, int lowerLetter, int digit)
        {
            return new RandomString(upperLetter, lowerLetter, digit, specialChars: 5);
        }

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter, int lowerLetter, int digit, int specialChars)
        {
            return new RandomString(upperLetter, lowerLetter, digit, specialChars);
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

        public static IDataGenerator<string> LoremIpsum(this DataGenerator<string> property, int sentenceCount = 3)
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
        #endregion

        #region Money
        public static IDataGenerator<decimal> Money(this DataGenerator<decimal> property, int digit)
        {
            return new MoneyGenerator(digit);
        }

        public static IDataGenerator<decimal> Money(this DataGenerator<decimal> property)
        {
            return new MoneyGenerator(multiplier: 1000);
        }
        #endregion

        #region AutoIncrement
        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property)
        {
            return new AutoIncrementDataGenerator(int.MinValue, int.MaxValue);
        }

        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property, int start)
        {
            return new AutoIncrementDataGenerator(start, 1);
        }

        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property, int start, int increment)
        {
            return new AutoIncrementDataGenerator(start, increment);
        }
        #endregion

        public static IDataGenerator<Guid> Guid(this DataGenerator<Guid> property)
        {
            return new GuidGenerator();
        }

        public static IDataGenerator<DateTime> Random(this DataGenerator<DateTime> property)
        {
            return new DateTimeGenerator();
        }

        public static IDataGenerator<TProperty> ComplexObject<TProperty>(this DataGenerator<TProperty> property, MockDataGenerator<TProperty> data) where TProperty : class, new()
        {
            return new ComplexObject<TProperty>(data);
        }

        #region Complex List
        public static IDataGenerator<List<TProperty>> ComplexList<TProperty>(this DataGenerator<TProperty> property, MockDataGenerator<TProperty> data) where TProperty : IList
        {
            return new ComplexList<TProperty>(data, count: 10);
        }

        public static IDataGenerator<List<TProperty>> ComplexList<TProperty>(this DataGenerator<TProperty> property, MockDataGenerator<TProperty> data, int count = 10) where TProperty : IList
        {
            return new ComplexList<TProperty>(data, count);
        }
        #endregion
    }
}
