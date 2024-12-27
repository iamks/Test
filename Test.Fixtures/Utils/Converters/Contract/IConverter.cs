namespace Test.Fixtures.Utils.Converters.Contract
{
    public interface IConverter
    {
        Type Type { get; }
        
        Task<object?> Convert(string fitnesseValue);
    }
}
