using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class FitnesseIdEntryConverter<TDto> : IConverter
    {
        public Type Type => typeof(TDto);

        public async Task<object?> Convert(string fitnesseValue)
        {
            if (fitnesseValue.IsFitnesseValueNull())
            {
                return null;
            }

            return DecisionTableEntriesManager.GetEntry<TDto>(fitnesseValue);
        }
    }
}
