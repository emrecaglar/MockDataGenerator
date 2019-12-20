using System.Collections.Generic;

namespace Mocking.DataGenerator.Generators
{
    public class ComplexList<T> : RandomizerBase, IDataGenerator<List<T>>
    {
        private readonly MockDataGenerator<T> _data;

        private readonly int _count;

        public ComplexList(MockDataGenerator<T> data, int count = 10)
        {
            _data = data;

            _count = count;
        }

        public List<T> Get()
        {
            return _data.Generate(_count);
        }
    }
}
