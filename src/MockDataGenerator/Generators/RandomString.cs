using System;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class RandomString : RandomizerBase, IDataGenerator<string>
    {
        const string UPPER_LETTER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string LOWER_LETTER = "abcdefghijklmnopqrstuvwxyz";
        const string DIGITS = "0123456789";
        const string SPECIAL_CHARS = "é'!^#+$%&/{}()[]=?*-_@<>|";

        private readonly int _upperLetter;
        private readonly int _lowerLetter;
        private readonly int _digit;
        private readonly int _specialChars;

        public RandomString(
            int upperLetter = 5,
            int loweLetter = 5,
            int digit = 5,
            int specialChars = 5)
        {
            _upperLetter = upperLetter;
            _lowerLetter = loweLetter;
            _digit = digit;
            _specialChars = specialChars;
        }

        public string Get()
        {
            var builder = new StringBuilder();

            if (_upperLetter > 0)
            {
                builder.Append(new string(UPPER_LETTER.OrderBy(x => Guid.NewGuid()).ToArray()).Substring(0, _upperLetter));
            }

            if (_lowerLetter > 0)
            {
                builder.Append(new string(LOWER_LETTER.OrderBy(x => Guid.NewGuid()).ToArray()).Substring(0, _lowerLetter));
            }

            if (_digit > 0)
            {
                builder.Append(new string(DIGITS.OrderBy(x => Guid.NewGuid()).ToArray()).Substring(0, _digit));
            }

            if (_specialChars > 0)
            {
                builder.Append(new string(SPECIAL_CHARS.OrderBy(x => Guid.NewGuid()).ToArray()).Substring(0, _specialChars));
            }

            return new string(builder.ToString().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
