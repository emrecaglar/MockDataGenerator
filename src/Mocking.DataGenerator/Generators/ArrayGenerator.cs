using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class PrimitiveArrayGenerator<TProperty> : RandomizerBase, IDataGenerator<TProperty[]>
    {
        private readonly int _count;

        public PrimitiveArrayGenerator(int count)
        {
            _count = count;
        }

        public TProperty[] Get()
        {
            return PrimitiveEnumerableHelper.Generate<TProperty>(_count);
        }
    }

    public class ArrayGenerator<TProperty> : RandomizerBase, IDataGenerator<TProperty[]>
    {
        private readonly MockDataGenerator<TProperty> _mocker;
        private readonly int _count;

        public ArrayGenerator(MockDataGenerator<TProperty> mocker, int count)
        {
            _mocker = mocker;
            _count = count;
        }

        public TProperty[] Get()
        {
            return _mocker.Generate(_count).ToArray();
        }
    }
}
