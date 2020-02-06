using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class ComplexObject<T> : RandomizerBase, IDataGenerator<T>
    {
        private readonly MockDataGenerator<T> _data;

        public ComplexObject(MockDataGenerator<T> data)
        {
            _data = data;
        }

        public T Get(CultureInfo culture)
        {
            return _data.GenerateOne();
        }
    }
}
