using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class DataGeneratorsTests
    {
        [Fact]
        public void CurrencySymbolTest()
        {
            var turkishCulture = new CultureInfo("tr-TR");
            var usaCulture = new CultureInfo("en-US");

            var turkey = new RegionInfo(turkishCulture.LCID);
            var usa = new RegionInfo(usaCulture.LCID);

            var generator = new CurrencySymbolGenerator();

            string symbol = generator.Get(turkishCulture);
        }
    }
}
