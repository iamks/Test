using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class StringConverter : IConverter
    {
        public Type Type => typeof(string);

        public async Task<object?> Convert(string fitnesseValue)
        {
            return fitnesseValue.IsFitnesseValueNull() 
                ? null 
                : fitnesseValue;
        }
    }
}
