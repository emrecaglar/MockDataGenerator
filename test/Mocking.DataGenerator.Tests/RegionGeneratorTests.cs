using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
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

            string region = regionGenerator.Get();

            Assert.NotNull(region);
        }
    }
}
