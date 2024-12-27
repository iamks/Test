using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class BoolConverter : IConverter
    {
        public Type Type => typeof(bool);

        public async Task<object?> Convert(string fitnesseValue)
        {
            return fitnesseValue.IsFitnesseValueNull() 
                ? null 
                : bool.Parse(fitnesseValue); 
        }
    }
}
