using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class CultureGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                                      .Select(x => x.Name)
                                      .ToArray();

            return cultures[Randomizer.Next(0, cultures.Length - 1)];
        }
    }
}
