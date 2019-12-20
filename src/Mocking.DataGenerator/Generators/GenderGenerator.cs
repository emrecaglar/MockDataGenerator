using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class GenderGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string[] _genders = new[] { "Male", "Female", "Unknown" };

        public string Get()
        {
            return _genders[Randomizer.Next(0, _genders.Length - 1)];
        }
    }
}
