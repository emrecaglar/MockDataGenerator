using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class RegionGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            return new RegionInfo(culture.LCID).DisplayName;
        }
    }
}
