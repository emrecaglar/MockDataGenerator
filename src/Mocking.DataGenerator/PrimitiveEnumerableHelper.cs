using Mocking.DataGenerator.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator
{
    internal class PrimitiveEnumerableHelper
    {
        public static T Generate<T>(Type elementType, int count)
        {
            var generatorInstance = PrimitiveDataGeneratorMap[elementType];

            var methodInfo = generatorInstance.GetType().GetMethod("Get");

            var data = Repeat(() =>
            {
                var value = methodInfo.Invoke(generatorInstance, new object[] { });

                return Convert.ChangeType(value, elementType);
            }, count);

            return Cast<T>(elementType, data);
        }

        private static T Cast<T>(Type elementType,  List<object> items)
        {
            var castMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast), new[] { typeof(IEnumerable) }).MakeGenericMethod(elementType);

            var list = castMethod.Invoke(null, new object[] { items });

            var toListMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToList)).MakeGenericMethod(elementType);

            return (T)toListMethod.Invoke(null, new object[] { list });
        }

        private static Dictionary<Type, object> PrimitiveDataGeneratorMap = new Dictionary<Type, object>
        {
            { typeof(int), new IntegerGenerator() },
            { typeof(uint), new IntegerGenerator() },
            { typeof(long), new LongGenerator() },
            { typeof(ulong), new LongGenerator() },
            { typeof(byte), new ByteGenerator() },
            { typeof(sbyte), new ByteGenerator() },
            { typeof(short), new ShortGenerator() },
            { typeof(ushort), new ShortGenerator() },
            { typeof(DateTime), new DateTimeGenerator() },
            { typeof(Guid), new GuidGenerator() },
            { typeof(string), new RandomStringGenerator() },
            { typeof(decimal), new MoneyGenerator(decimal.MinValue, decimal.MaxValue) },
            { typeof(double), new MoneyGenerator(decimal.MinValue, decimal.MaxValue) },
            { typeof(float), new MoneyGenerator(decimal.MinValue, decimal.MaxValue) }
        };

        private static List<T> Repeat<T>(Func<T> executer, int count)
        {
            var list = new List<T>();

            for (int i = 0; i < count; i++)
            {
                list.Add(executer());
            }

            return list;
        }
    }
}
