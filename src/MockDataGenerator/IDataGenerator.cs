namespace Mocking.DataGenerator
{
    public interface IDataGenerator<out TProperty>
    {
        TProperty Get();
    }
}
