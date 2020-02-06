using System;
using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class DateTimeGenerator : RandomizerBase, IDataGenerator<DateTime>
    {
        public DateTime Get(CultureInfo culture)
        {
            return DateTime.Now.AddDays(Randomizer.Next(-1500, 1500)).AddSeconds(Randomizer.Next(-15000, 15000));
        }
    }

    public class NullableDateTimeGenerator : DateTimeGenerator, IDataGenerator<DateTime?>
    {
        public new DateTime? Get(CultureInfo culture)
        {
            return base.Get(culture);
        }
    }

    public class StringDateTimeGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string _format;

        public StringDateTimeGenerator(string format = null)
        {
            _format = format;
        }

        public string Get(CultureInfo culture)
        {
            var dateTime = DateTime.Now.AddDays(Randomizer.Next(-1500, 1500)).AddSeconds(Randomizer.Next(-15000, 15000));

            return _format == null
                        ? dateTime.ToString(culture)
                        : dateTime.ToString(_format);
        }
    }
}
