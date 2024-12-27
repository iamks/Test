using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class IntConverter : IConverter
    {
        public Type Type => typeof(int);

        public async Task<object?> Convert(string fitnesseValue)
        {
            return fitnesseValue.IsFitnesseValueNull() 
                ? null 
                : int.Parse(fitnesseValue); 
        }
    }
}
