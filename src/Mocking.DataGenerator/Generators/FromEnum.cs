using System;
using System.Globalization;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class FromEnum<TEnum> : RandomizerBase, IDataGenerator<TEnum> where TEnum : Enum
    {
        public TEnum Get(CultureInfo culture)
        {
            var values = Enum.GetValues(typeof(TEnum));

            return values.Cast<TEnum>().ToList()[Randomizer.Next(0, values.Length)];
        }
    }

    public class NullableFromEnum<TEnum> : RandomizerBase, IDataGenerator<TEnum?> where TEnum : struct, Enum
    {
        public TEnum? Get(CultureInfo culture)
        {
            var values = Enum.GetValues(typeof(TEnum));

            return values.Cast<TEnum>().ToList()[Randomizer.Next(0, values.Length)];
        }
    }
}
