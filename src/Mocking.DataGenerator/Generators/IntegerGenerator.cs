using System.Globalization;

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

        public int Get(CultureInfo culture)
        {
            return Randomizer.Next(_min, _max);
        }
    }

    public class NullableIntegerGenerator : IntegerGenerator, IDataGenerator<int?>
    {
        public NullableIntegerGenerator(int min = int.MinValue, int max = int.MaxValue):base(min, max) { }

        public new int? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }
}
