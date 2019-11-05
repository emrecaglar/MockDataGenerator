using System;

namespace MockDataGenerator.DataGenerators
{
    public class RandomBoolean : RandomizerBase, IDataGenerator<bool>
    {
        private readonly Random _randomizer = new Random();

        public bool Get()
        {
            return Convert.ToBoolean(_randomizer.Next(0, 1));
        }
    }
}
