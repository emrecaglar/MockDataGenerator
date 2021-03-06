﻿using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class MoneyGeneratorTests
    {
        [Fact]
        public void MoneyGenerator_ShoulBeMoneyGenerate()
        {
            var generator1 = new DecimalGenerator();
            var money1 = generator1.Get(new CultureInfo("en-US"));

            var generator2 = new DecimalGenerator();
            var money2 = generator2.Get(new CultureInfo("en-US"));
        }
    }
}
