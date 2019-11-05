using System;
using System.Linq;

namespace MockDataGenerator.DataGenerators
{
    public class FromEnum<TEnum> : RandomizerBase, IDataGenerator<TEnum> where TEnum : struct
    {
        public TEnum Get()
        {
            var values = Enum.GetValues(typeof(Enum));

            return values.Cast<TEnum>().ToList()[Randomizer.Next(0, values.Length - 1)];
        }
    }
}
