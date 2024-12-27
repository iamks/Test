using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class GuidConverter : IConverter
    {
        public Type Type => typeof(Guid);

        public async Task<object?> Convert(string fitnesseValue)
        {
            return fitnesseValue.IsFitnesseValueNull()
                ? null
                : Guid.Parse(fitnesseValue);
        }
    }
}
