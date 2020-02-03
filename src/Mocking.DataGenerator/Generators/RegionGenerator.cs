using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class RegionGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly CultureInfo[] _cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToArray();

        public string Get()
        {
            return new RegionInfo(_cultures[Randomizer.Next(0, _cultures.Length - 1)].LCID).DisplayName;
        }
    }
}
