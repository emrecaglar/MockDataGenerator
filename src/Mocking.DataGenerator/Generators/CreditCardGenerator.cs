using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class CreditCardGenerator : RandomizerBase, IDataGenerator<string>
    {
        //https://www.codementor.io/@etharalali/generating-test-credit-card-numbers-fast-773983p70
        public string Get(CultureInfo culture)
        {
            int[] checkArray = new int[15];

            var cardNum = new int[16];

            for (int d = 14; d >= 0; d--)
            {
                cardNum[d] = Randomizer.Next(0, 9);
                checkArray[d] = (cardNum[d] * (((d + 1) % 2) + 1)) % 9;
            }

            cardNum[15] = (checkArray.Sum() * 9) % 10;

            var sb = new StringBuilder();

            for (int d = 0; d < 16; d++)
            {
                sb.Append(cardNum[d].ToString());
            }
            return sb.ToString();
        }
    }
}
