using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class MoneyGeneratorTests
    {
        [Fact]
        public void MoneyGenerator_ShoulBeMoneyGenerate()
        {
            var generator1 = new MoneyGenerator(5, 25);
            var money1 = generator1.Get();

            var generator2 = new MoneyGenerator(10, 500);
            var money2 = generator2.Get();

            Assert.True(money1 > 5 && money1 < 25);
            Assert.True(money2 > 10 && money2 < 500);
        }
    }
}
