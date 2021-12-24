using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class LoremIpsumGenerator : RandomizerBase, IDataGenerator<string>
    {
        private const string ORIGINAL = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        private readonly int _sentenceCount;
        private readonly int _paragraphCount;

        public LoremIpsumGenerator(int sentenceCount = 3, int paragraphCount = 1)
        {
            _sentenceCount = sentenceCount;

            _paragraphCount = paragraphCount;
        }

        public string Get(CultureInfo culture)
        {
            List<string> sentence = ORIGINAL.Split(new char[] { '.' }).ToList();

            var builder = new StringBuilder();

            if (_sentenceCount < sentence.Count)
            {
                sentence = sentence.Take(_sentenceCount).ToList();
            }
            else if (_sentenceCount > sentence.Count)
            {
                int subtract = _sentenceCount - sentence.Count;
                var originalSentences = ORIGINAL.Split(new char[] { '.' });

                for (int i = 0; i < subtract; i++)
                {
                    sentence.Add(originalSentences[Randomizer.Next(0, originalSentences.Length - 1)]);
                }
            }

            for (int p = 0; p < _paragraphCount; p++)
            {

                builder.AppendLine(string.Concat(sentence.OrderBy(x => Guid.NewGuid())));
            }

            return builder.ToString();
        }
    }
}
