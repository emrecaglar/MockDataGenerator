using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class CreditCardTests
    {
        [Fact]
        public void CreditCardGenerator_ShouldBeGenerateCreditCard()
        {
            var generator = new CreditCardGenerator();

            string cardNumber = generator.Get(new CultureInfo("en-US"));

            Assert.True(Luhn(cardNumber));
        }

        public static bool Luhn(string digits)
        {
            return digits.All(char.IsDigit) && digits.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }
    }
}
