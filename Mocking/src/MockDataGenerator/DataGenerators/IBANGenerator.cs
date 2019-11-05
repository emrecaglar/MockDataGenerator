namespace MockDataGenerator.DataGenerators
{
    public class IBANGenerator : RandomizerBase, IDataGenerator<string>
    {
        public string Get()
        {
            return $"TR{Randomizer.Next(10, 99)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(1000, 9999)} {Randomizer.Next(10, 99)}";
        }
    }
}
