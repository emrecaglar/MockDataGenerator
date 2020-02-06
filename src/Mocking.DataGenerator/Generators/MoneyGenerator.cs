using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    //https://stackoverflow.com/questions/22046831/random-numbers-with-decimals?lq=1
    public class MoneyGenerator : RandomizerBase, IDataGenerator<decimal>
    {
        private readonly decimal _min;
        private readonly decimal _max;

        public MoneyGenerator(decimal min, decimal max)
        {
            _min = min;
            _max = max;
        }

        public decimal Get()
        {
            return Math.Round((decimal)Randomizer.NextDouble() * ((decimal)(_max - _min)) + _min, 2);
        }
    }

    public class NullableMoneyGenerator : MoneyGenerator, IDataGenerator<decimal?>
    {
        public NullableMoneyGenerator(decimal min, decimal max):base(min, max) { }

        public new decimal? Get()
        {
            return base.Get();
        }
    }
}
