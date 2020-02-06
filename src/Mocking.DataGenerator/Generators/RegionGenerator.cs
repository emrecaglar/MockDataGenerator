using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class RegionGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly CultureInfo[] _cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        public string Get()
        {
            var culture = _cultures[Randomizer.Next(0, _cultures.Length)];

            return new RegionInfo(culture.LCID).DisplayName;
        }
    }
}
