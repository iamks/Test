using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class DecimalConverter : IConverter
    {
        public Type Type => typeof(float);

        public async Task<object?> Convert(string fitnesseValue)
        {
            return fitnesseValue.IsFitnesseValueNull()
                ? null
                : decimal.Parse(fitnesseValue);
        }
    }
}
