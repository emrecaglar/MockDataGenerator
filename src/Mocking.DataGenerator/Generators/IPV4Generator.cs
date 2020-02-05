namespace Mocking.DataGenerator.Generators
{
    public class IPV4Generator : RandomizerBase, IDataGenerator<string>
    {
        public string Get()
        {
            return $"{Randomizer.Next(1, 256)}.{Randomizer.Next(1, 256)}.{Randomizer.Next(1, 256)}.{Randomizer.Next(1, 256)}";
        }
    }
}
