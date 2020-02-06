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
        public static TProperty[] Generate<TProperty>(int count)
        {
            var generatorInstance = PrimitiveDataGeneratorMap[typeof(TProperty)];

            var methodInfo = generatorInstance.GetType().GetMethod("Get");

            var data = Repeat(() => 
            {
                return methodInfo.Invoke(generatorInstance, new object[] { });
            }, count);

            return data.Cast<TProperty>().ToArray();
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
            { typeof(float), new MoneyGenerator(decimal.MinValue, decimal.MaxValue) },
        };

        private static T[] Repeat<T>(Func<T> executer, int count)
        {
            var list = new List<T>();

            for (int i = 0; i < count; i++)
            {
                list.Add(executer());
            }

            return list.ToArray();
        }
    }
}
