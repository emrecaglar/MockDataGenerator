using System.Globalization;

namespace Mocking.DataGenerator
{
    public interface IDataGenerator<out TProperty>
    {
        TProperty Get(CultureInfo culture);
    }
}
