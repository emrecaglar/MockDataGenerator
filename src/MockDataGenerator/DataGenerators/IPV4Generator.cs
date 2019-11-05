namespace MockDataGenerator.DataGenerators
{
    public class IPV4Generator : RandomizerBase, IDataGenerator<string>
    {
        public string Get()
        {
            return $"{Randomizer.Next(1, 255)}.{Randomizer.Next(1, 255)}.{Randomizer.Next(1, 255)}.{Randomizer.Next(1, 255)}";
        }
    }
}
