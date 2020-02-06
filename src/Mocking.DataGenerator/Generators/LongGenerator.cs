using System;
using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    //https://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way/6651661
    public class LongGenerator : RandomizerBase, IDataGenerator<long>
    {
        private readonly long _min;
        private readonly long _max;

        public LongGenerator(long min = long.MinValue, long max = long.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public long Get(CultureInfo culture)
        {
            byte[] buf = new byte[8];
            Randomizer.NextBytes(buf);

            long longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (_max - _min)) + _min;
        }
    }

    public class NullableLongGenerator : LongGenerator, IDataGenerator<long?>
    {
        public NullableLongGenerator(long min = long.MinValue, long max = long.MaxValue) : base(min, max) { }

        public new long? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }
}
