using System.Globalization;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class IBANGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            var region = new RegionInfo(culture.LCID).TwoLetterISORegionName;

            return $"{region.ToUpper()}{Randomizer.Next(10, 99)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(10, 99)}";
        }
    }
}
