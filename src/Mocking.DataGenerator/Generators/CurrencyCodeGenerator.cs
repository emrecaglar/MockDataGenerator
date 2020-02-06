using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class CurrencyCodeGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            return new RegionInfo(culture.LCID).ISOCurrencySymbol;
        }
    }
}
