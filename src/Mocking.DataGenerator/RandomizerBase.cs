using System;

namespace Mocking.DataGenerator
{
    public abstract class RandomizerBase
    {
        protected Random Randomizer { get; private set; } = new Random();
    }
}
