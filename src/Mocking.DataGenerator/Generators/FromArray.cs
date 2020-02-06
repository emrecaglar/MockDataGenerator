using System;
using System.Globalization;

namespace Mocking.DataGenerator.Generators
{
    public class FromArray<T> : RandomizerBase, IDataGenerator<T>
    {
        private readonly T[] _arr;

        public FromArray(T[] arr)
        {
            _arr = arr;
        }

        public T Get(CultureInfo culture)
        {
            return _arr[Randomizer.Next(0, _arr.Length)];
        }
    }

    public class NullableFromArray<T> : RandomizerBase, IDataGenerator<T?> where T : struct
    {
        private readonly T?[] _arr;

        public NullableFromArray(T?[] arr)
        {
            _arr = arr;
        }

        public T? Get(CultureInfo culture)
        {
            return _arr[Randomizer.Next(0, _arr.Length)];
        }
    }
}
