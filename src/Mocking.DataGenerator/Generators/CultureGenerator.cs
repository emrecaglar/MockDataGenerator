using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class CultureGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            return culture.Name;
        }
    }
}
