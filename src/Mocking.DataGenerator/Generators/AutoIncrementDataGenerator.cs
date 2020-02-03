using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.DataGenerator.Generators
{
    public class AutoIncrementDataGenerator : IDataGenerator<int>
    {
        private readonly int _start;
        private readonly int _increment;

        private int _current;

        public AutoIncrementDataGenerator(int start = 1, int increment = 1)
        {
            _increment = increment;
            _start = start;

            _current = _start;
        }

        public int Get()
        {
            int data = _current;

            _current += _increment;

            return data;
        }
    }

    public class NullableAutoIncrementDataGenerator : AutoIncrementDataGenerator, IDataGenerator<int?>
    {
        public NullableAutoIncrementDataGenerator(int start = 1, int increment = 1):base(start, increment)
        {
            
        }

        public new int? Get()
        {
            return base.Get();
        }
    }
}
