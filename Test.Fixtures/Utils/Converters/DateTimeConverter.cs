using System.Globalization;
using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class DateTimeConverter : IConverter
    {
        private const string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        public Type Type => typeof(DateTime);

        public async Task<object?> Convert(string fitnesseValue)
        {
            object? parsedDateTime = null;
            if (!fitnesseValue.IsFitnesseValueNull())
            {
                try
                {
                    parsedDateTime = DateTime.ParseExact(fitnesseValue, DATETIME_FORMAT, CultureInfo.InvariantCulture);
                }
                catch(FormatException ex)
                {
                    throw new FormatException("The string was not recognized as a valid DateTime. The expected format is yyyy-MM-ddTHH:mm:ss.", ex);
                }
            }

            return parsedDateTime;
        }
    }
}
