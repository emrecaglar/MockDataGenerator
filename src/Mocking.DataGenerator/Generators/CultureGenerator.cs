using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class CultureGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string[] _cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => x.Name).ToArray();

        public string Get()
        {
            return _cultures[Randomizer.Next(0, _cultures.Length)];
        }
    }
}
