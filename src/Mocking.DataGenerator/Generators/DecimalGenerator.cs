using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    //https://stackoverflow.com/questions/22046831/random-numbers-with-decimals?lq=1
    public class DecimalGenerator : RandomizerBase, IDataGenerator<decimal>
    {
        public decimal Get(CultureInfo culture)
        {
            return (decimal)Math.Round(Randomizer.NextDouble() * 1000, 2);
        }
    }

    public class NullableDecimalGenerator : DecimalGenerator, IDataGenerator<decimal?>
    {
        public NullableDecimalGenerator():base() { }

        public new decimal? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }
}
