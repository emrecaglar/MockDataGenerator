using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class ListGenerator<TProperty, TElement> : RandomizerBase, IDataGenerator<TProperty> where TProperty : class
    {
        private readonly MockDataGenerator<TElement> _data;

        private readonly int _count;

        public ListGenerator(MockDataGenerator<TElement> data, int count = 10)
        {
            _data = data;

            _count = count;
        }

        public TProperty Get(CultureInfo culture)
        {
            return _data.Generate(_count) as TProperty;
        }
    }

    public class PrimitiveListGenerator<T> : RandomizerBase, IDataGenerator<T> where T : class
    {
        private readonly int _count;

        public PrimitiveListGenerator(int count = 10)
        {
            _count = count;
        }

        public T Get(CultureInfo culture)
        {
            var elementType = typeof(T).GetGenericArguments()[0];

            return PrimitiveEnumerableHelper.Generate<T>(elementType, culture, _count);
        }
    }
}
