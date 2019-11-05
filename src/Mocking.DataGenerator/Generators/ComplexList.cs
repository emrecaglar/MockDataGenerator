using System.Collections.Generic;

namespace Mocking.DataGenerator.Generators
{
    public class ComplexList<T> : RandomizerBase, IDataGenerator<List<T>>
    {
        private readonly MockDataGenerator<T> _data;

        public ComplexList(MockDataGenerator<T> data)
        {
            _data = data;
        }

        public List<T> Get()
        {
            return _data.Generate();
        }
    }
}
