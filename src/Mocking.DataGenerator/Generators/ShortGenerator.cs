using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class ShortGenerator : RandomizerBase, IDataGenerator<short>
    {
        private readonly short _min;
        private readonly short _max;

        public ShortGenerator(short min = short.MinValue, short max = short.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public short Get(CultureInfo culture)
        {
            return (short)Randomizer.Next(_min, _max);
        }
    }

    public class NullableShortGenerator : IntegerGenerator, IDataGenerator<short?>
    {
        public NullableShortGenerator(short min = short.MinValue, short max = short.MaxValue) : base(min, max) { }

        public new short? Get(CultureInfo culture)
        {
            return (short)base.Get(culture);
        }
    }
}
