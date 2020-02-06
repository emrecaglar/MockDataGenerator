using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Mocking.DataGenerator.Tests
{
    public class RegionGeneratorTests
    {
        [Fact]
        public void RegionGeneratorTest()
        {
            var regionGenerator = new RegionGenerator();

            string region = regionGenerator.Get(new CultureInfo("en-US"));

            Assert.NotNull(region);
        }
    }
}
