using System;

namespace Mocking.DataGenerator.Generators
{
    public class FromArray<T> : RandomizerBase, IDataGenerator<T>
    {
        private readonly T[] _arr;

        public FromArray(T[] arr)
        {
            _arr = arr;
        }

        public T Get()
        {
            return _arr[Randomizer.Next(0, _arr.Length - 1)];
        }
    }

    public class NullableFromArray<T> : RandomizerBase, IDataGenerator<T?> where T : struct
    {
        private readonly T?[] _arr;

        public NullableFromArray(T?[] arr)
        {
            _arr = arr;
        }

        public T? Get()
        {
            return _arr[Randomizer.Next(0, _arr.Length - 1)];
        }
    }
}
