namespace MockDataGenerator
{
    public interface IDataGenerator<out TProperty>
    {
        TProperty Get();
    }
}
