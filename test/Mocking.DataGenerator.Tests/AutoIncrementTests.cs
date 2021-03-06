﻿using Mocking.DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Mocking.DataGenerator;

namespace Mocking.DataGenerator.Tests
{
    public class AutoIncrementTests
    {
        [Fact]
        public void AutoIncrement_ShoulBeIncrement()
        {
            var data = new MockDataGenerator<MyType>()
                            .Register(x => x.Value, x => x.AutoIncrement(10, 10))
                            .Register(x=>x.NullableValue, x=>x.Random())
                            .Generate(count: 5);


            Assert.Equal(10, data[0].Value);
            Assert.Equal(20, data[1].Value);
            Assert.Equal(30, data[2].Value);
            Assert.Equal(40, data[3].Value);
            Assert.Equal(50, data[4].Value);
        }
    }

    public class MyType
    {
        public int Value { get; set; }

        public int? NullableValue { get; set; }
    }
}
