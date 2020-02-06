using System;
using System.Collections.Generic;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class ListGenerator<T> : RandomizerBase, IDataGenerator<List<T>>
    {
        private readonly MockDataGenerator<T> _data;

        private readonly int _count;

        public ListGenerator(MockDataGenerator<T> data, int count = 10)
        {
            _data = data;

            _count = count;
        }

        public List<T> Get()
        {
            return _data.Generate(_count);
        }
    }

    public class PrimitiveListGenerator<T> : RandomizerBase, IDataGenerator<List<T>>
    {
        private readonly int _count;

        public PrimitiveListGenerator(int count = 10)
        {
            _count = count;
        }

        public List<T> Get()
        {
            return PrimitiveEnumerableHelper.Generate<T>(_count).ToList();
        }
    }
}
