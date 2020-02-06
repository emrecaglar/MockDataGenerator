using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public string Get(CultureInfo culture)
        {
            string fmt = _format ?? "+#(###)###-##-##";

            int digitCount = fmt.Count(x => x == '#');

            if (digitCount > 19)
            {
                throw new Exception("invalid format: max 19 digit use");
            }

            var builder = new StringBuilder();
            for (int i = 0; i < digitCount; i++)
            {
                builder.Append(Randomizer.Next(1, 10));
            }

            var phoneNumber = long.Parse(builder.ToString());

            return phoneNumber.ToString(fmt);
        }
    }
}
