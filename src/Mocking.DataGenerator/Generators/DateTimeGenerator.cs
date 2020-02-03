using System;

namespace Mocking.DataGenerator.Generators
{
    public class DateTimeGenerator : RandomizerBase, IDataGenerator<DateTime>
    {
        public DateTime Get()
        {
            return DateTime.Now.AddDays(Randomizer.Next(-1500, 1500)).AddSeconds(Randomizer.Next(-15000, 15000));
        }
    }

    public class NullableDateTimeGenerator : DateTimeGenerator, IDataGenerator<DateTime?>
    {
        public new DateTime? Get()
        {
            return base.Get();
        }
    }
}
