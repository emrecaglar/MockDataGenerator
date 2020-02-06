using System;
using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class RandomBoolean : RandomizerBase, IDataGenerator<bool>
    {
        private readonly Random _randomizer = new Random();

        public bool Get(CultureInfo culture)
        {
            return Convert.ToBoolean(_randomizer.Next(0, 2));
        }
    }

    public class NullableRandomBoolean : RandomBoolean, IDataGenerator<bool?>
    {
        public new bool? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }
}
