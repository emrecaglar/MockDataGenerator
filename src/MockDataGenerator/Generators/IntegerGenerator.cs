namespace Mocking.DataGenerator.Generators
{
    public class IntegerGenerator : RandomizerBase, IDataGenerator<int>
    {
        private readonly int _min;
        private readonly int _max;

        public IntegerGenerator(int min = int.MinValue, int max = int.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public int Get()
        {
            return Randomizer.Next(_min, _max);
        }
    }
}
