using System;

namespace MockDataGenerator
{
    public abstract class RandomizerBase
    {
        protected Random Randomizer { get; private set; } = new Random();
    }
}
