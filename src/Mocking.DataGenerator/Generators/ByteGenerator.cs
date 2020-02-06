using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class ByteGenerator : RandomizerBase, IDataGenerator<byte>
    {
        private readonly byte _min;
        private readonly byte _max;

        public ByteGenerator(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public byte Get(CultureInfo culture)
        {
            return (byte)Randomizer.Next(_min, _max);
        }
    }

    public class NullableByteGenerator : ByteGenerator, IDataGenerator<byte?>
    {
        public NullableByteGenerator(byte min = byte.MinValue, byte max = byte.MaxValue) : base(min, max) { }

        public new byte? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }
}
