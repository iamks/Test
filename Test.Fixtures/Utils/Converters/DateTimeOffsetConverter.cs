using System.Globalization;
using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class DateTimeOffsetConverter : IConverter
    {
        private const string DATETIMEOFFSET_FORMAT = "yyyy-MM-ddTHH:mm:sszzz";

        public Type Type => typeof(DateTimeOffset);

        public async Task<object?> Convert(string fitnesseValue)
        {
            object? parsedDateTime = null;
            if (!fitnesseValue.IsFitnesseValueNull())
            {
                try
                {
                    parsedDateTime = DateTimeOffset.ParseExact(fitnesseValue, DATETIMEOFFSET_FORMAT, CultureInfo.InvariantCulture);
                }
                catch (FormatException ex)
                {
                    throw new FormatException("The string was not recognized as a valid DateTime. The expected format is yyyy-MM-ddTHH:mm:sszzz.", ex);
                }
            }

            return parsedDateTime;
        }
    }
}
