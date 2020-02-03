using System;

namespace Mocking.DataGenerator.Generators
{
    public class GuidGenerator : RandomizerBase, IDataGenerator<Guid>
    {
        public Guid Get()
        {
            return Guid.NewGuid();
        }
    }

    public class NullableGuidGenerator : GuidGenerator, IDataGenerator<Guid?>
    {
        public Guid? Get()
        {
            return base.Get();
        }
    }
}
