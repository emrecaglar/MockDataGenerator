using System;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class FromEnum<TEnum> : RandomizerBase, IDataGenerator<TEnum> where TEnum : Enum
    {
        public TEnum Get()
        {
            var values = Enum.GetValues(typeof(TEnum));

            return values.Cast<TEnum>().ToList()[Randomizer.Next(0, values.Length - 1)];
        }
    }
}
