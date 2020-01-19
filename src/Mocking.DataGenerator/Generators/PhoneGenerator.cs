using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class PhoneGenerator : RandomizerBase, IDataGenerator<string>
    {
        private readonly string _format;

        public PhoneGenerator(string format = null)
        {
            _format = format;
        }
             
        public string Get()
        {
            int countryCode = Randomizer.Next(1, 9);
            int digits = Randomizer.Next(349586710, 876586959);
            int lastDigit = Randomizer.Next(0, 9);

            long phone;
            if (_format.Contains('+'))
            {
                phone = long.Parse($"{countryCode}{digits}{lastDigit}");
            }
            else
            {
                phone = long.Parse($"{digits}{lastDigit}");
            }
            
            return phone.ToString(_format);
        }
    }
}
