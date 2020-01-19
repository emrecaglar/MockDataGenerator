using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class MoneyGenerator : RandomizerBase, IDataGenerator<decimal>
    {
        private readonly int _multiplier;

        public MoneyGenerator(int? multiplier = null)
        {
            _multiplier = multiplier ?? 1000;
        }

        public decimal Get()
        {
            return Math.Round((decimal)(Randomizer.NextDouble() * 10 * _multiplier), 2);
        }
    }
}
