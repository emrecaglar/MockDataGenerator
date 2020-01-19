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
            var generator1 = new MoneyGenerator();
            var money1 = generator1.Get();

            var generator2 = new MoneyGenerator(100);
            var money2 = generator2.Get();

            Assert.True(money1 > 1000);
            Assert.True(money2 > 100 && money2 < 1000);
        }
    }
}
