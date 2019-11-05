using System;

namespace MockDataGenerator.DataGenerators
{
    public class DateTimeGenerator : RandomizerBase, IDataGenerator<DateTime>
    {
        public DateTime Get()
        {
            return DateTime.Now.AddDays(Randomizer.Next(-1500, 1500)).AddSeconds(Randomizer.Next(-15000, 15000));
        }
    }
}
