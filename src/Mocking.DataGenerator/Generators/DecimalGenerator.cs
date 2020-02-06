using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    //https://stackoverflow.com/questions/22046831/random-numbers-with-decimals?lq=1
    public class DecimalGenerator : RandomizerBase, IDataGenerator<decimal>
    {
        public decimal Get()
        {
            return Randomizer.NextDecimal() * 1000;
        }
    }

    public class NullableDecimalGenerator : DecimalGenerator, IDataGenerator<decimal?>
    {
        public NullableDecimalGenerator():base() { }

        public new decimal? Get()
        {
            return base.Get();
        }
    }
}
