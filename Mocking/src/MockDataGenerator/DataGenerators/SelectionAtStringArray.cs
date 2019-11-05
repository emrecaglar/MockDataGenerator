namespace MockDataGenerator.DataGenerators
{
    public class SelectionAtStringArray : RandomizerBase, IDataGenerator<string>
    {
        private readonly string[] _arr;

        public SelectionAtStringArray(string[] arr)
        {
            _arr = arr;
        }

        public string Get()
        {
            return _arr[Randomizer.Next(0, _arr.Length - 1)];
        }
    }
}
