using System;
using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class GuidGenerator : RandomizerBase, IDataGenerator<Guid>
    {
        public Guid Get(CultureInfo culture)
        {
            return Guid.NewGuid();
        }
    }

    public class NullableGuidGenerator : GuidGenerator, IDataGenerator<Guid?>
    {
        public Guid? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }

    public class StringGuidGenerator : GuidGenerator, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            return base.Get(culture).ToString();
        }
    }
}
