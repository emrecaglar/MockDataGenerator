using System;

namespace Mocking.DataGenerator.Generators
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
