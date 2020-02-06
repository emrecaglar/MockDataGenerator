using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class PrimitiveArrayGenerator<TProperty> : RandomizerBase, IDataGenerator<TProperty> where TProperty : class
    {
        private readonly int _count;

        public PrimitiveArrayGenerator(int count)
        {
            _count = count;
        }

        public TProperty Get()
        {
            var elementType = typeof(TProperty).GetElementType();

            return PrimitiveEnumerableHelper.Generate<TProperty>(elementType, _count);
        }
    }

    public class ArrayGenerator<TProperty, TElementType> : RandomizerBase, IDataGenerator<TProperty> where TProperty : class
    {
        private readonly MockDataGenerator<TElementType> _mocker;
        private readonly int _count;

        public ArrayGenerator(MockDataGenerator<TElementType> mocker, int count)
        {
            _mocker = mocker;
            _count = count;
        }

        public TProperty Get()
        {
            return _mocker.Generate(_count).ToArray() as TProperty;
        }
    }
}
