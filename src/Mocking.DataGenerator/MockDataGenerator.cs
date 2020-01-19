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

        public ResolverType Type { get; set; }
    }

    enum ResolverType
    {
        FromProperty,
        FromFactory
    }

    public class MockDataGenerator<TModel>
    {
        private readonly Dictionary<LambdaExpression, ValueResolver> _expressions = new Dictionary<LambdaExpression, ValueResolver>();

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Func<TProperty> valueResolver)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => valueResolver(), Type = ResolverType.FromProperty });
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => value, Type = ResolverType.FromProperty });
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, IDataGenerator<TProperty> randomData)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => randomData.Get(), Type = ResolverType.FromProperty });
            }

            return this;
        }

        public MockDataGenerator<TModel> Register<TProperty>(Expression<Func<TModel, TProperty>> expression, Expression<Func<DataGenerator<TProperty>, IDataGenerator<TProperty>>> factory)
        {
            if (!_expressions.ContainsKey(expression))
            {
                _expressions.Add(expression, new ValueResolver { Resolver = () => factory, Type = ResolverType.FromFactory });
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

                    pi.SetValue(model, GetValue(model, expression), new object[] { });
                }

                data.Add(model);
            }

            return data;
        }

        private object GetValue(object model, KeyValuePair<LambdaExpression, ValueResolver> dic)
        {
            switch (dic.Value.Type)
            {
                case ResolverType.FromProperty:
                    return FromProperty(model, dic.Key, dic.Value.Resolver);
                case ResolverType.FromFactory:
                    return FromFactory(model, dic.Key, dic.Value.Resolver);
            }

            throw new NotSupportedException($"{nameof(dic.Value.Resolver)}: {dic.Value.Resolver.ToString()}");
        }

        private object FromProperty(object model, LambdaExpression exp, Func<object> resolver)
        {
            return resolver();
        }

        private object FromFactory(object model, LambdaExpression exp, Func<object> resolver)
        {
            MemberExpression memberExpression = (MemberExpression)exp.Body;

            PropertyInfo propertyInfo = (PropertyInfo)memberExpression.Member;

            var factory = (LambdaExpression)resolver();

            var dataResolverFactory = Activator.CreateInstance(typeof(DataGenerator<>).MakeGenericType(propertyInfo.PropertyType));

            var dataGenerator = factory.Compile().DynamicInvoke(dataResolverFactory);

            var data = dataGenerator.GetType().GetMethod("Get", Type.EmptyTypes).Invoke(dataGenerator, new object[] { });

            return data;
        }
    }

    public abstract class DataGenerator<TProperty>
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

        public static IDataGenerator<string> Phone(this DataGenerator<string> property, string format = "+#(###)###-##-##")
        {
            return new PhoneGenerator(format);
        }

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

        public static IDataGenerator<string> Random(this DataGenerator<string> property, int upperLetter = 5, int lowerLetter = 5, int digit = 5, int specialChars = 5)
        {
            return new RandomString(upperLetter, lowerLetter, digit, specialChars);
        }

        public static IDataGenerator<string> MD5(this DataGenerator<string> property)
        {
            return new MD5Generator();
        }

        public static IDataGenerator<string> LoremIpsum(this DataGenerator<string> property, int sentenceCount = 3, int paragraphCount = 1)
        {
            return new LoremIpsumGenerator(sentenceCount, paragraphCount);
        }

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

        public static IDataGenerator<int> Random(this DataGenerator<int> property, int min = int.MinValue, int max = int.MaxValue)
        {
            return new IntegerGenerator(min, max);
        }

        public static IDataGenerator<decimal> Money(this DataGenerator<decimal> property, int digit = 1000)
        {
            return new MoneyGenerator(digit);
        }

        public static IDataGenerator<int> AutoIncrement(this DataGenerator<int> property, int start = 1, int increment = 1)
        {
            return new AutoIncrementDataGenerator(start, increment);
        }

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

        public static IDataGenerator<List<TProperty>> ComplexList<TProperty>(this DataGenerator<TProperty> property, MockDataGenerator<TProperty> data, int count = 10) where TProperty : IList
        {
            return new ComplexList<TProperty>(data, count);
        }
    }
}
