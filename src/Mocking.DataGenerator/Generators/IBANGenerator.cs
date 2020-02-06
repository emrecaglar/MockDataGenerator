using System.Globalization;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class IBANGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly CultureInfo[] _cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        public string Get()
        {
            var culture = _cultures[Randomizer.Next(0, _cultures.Length - 1)];

            var region = new RegionInfo(culture.Name).TwoLetterISORegionName;

            return $"{region.ToUpper()}{Randomizer.Next(10, 99)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(10, 99)}";
        }
    }
}
