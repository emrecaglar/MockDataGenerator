namespace MockDataGenerator.DataGenerators
{
    public class ComplexObject<T> : RandomizerBase, IDataGenerator<T>
    {
        private readonly MockDataGenerator<T> _data;

        public ComplexObject(MockDataGenerator<T> data)
        {
            _data = data;
        }

        public T Get()
        {
            return _data.Generate()[0];
        }
    }
}
