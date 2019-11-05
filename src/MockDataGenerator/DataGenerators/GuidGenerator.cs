using System;

namespace MockDataGenerator.DataGenerators
{
    public class GuidGenerator : RandomizerBase, IDataGenerator<Guid>
    {
        public Guid Get()
        {
            return Guid.NewGuid();
        }
    }
}
