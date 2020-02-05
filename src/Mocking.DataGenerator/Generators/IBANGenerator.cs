using System.Globalization;
using System.Linq;

namespace Mocking.DataGenerator.Generators
{
    public class IBANGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly CultureInfo[] _cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

        public string Get()
        {
            var region = new RegionInfo(_cultures[Randomizer.Next(0, _cultures.Length)].LCID).TwoLetterISORegionName;

            return $"{region.ToUpper()}{Randomizer.Next(10, 99)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(10, 99)}";
        }
    }
}
