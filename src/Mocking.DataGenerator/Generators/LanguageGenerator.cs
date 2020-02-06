using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class LanguageGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get(CultureInfo culture)
        {
            return culture.EnglishName;
        }
    }
}
