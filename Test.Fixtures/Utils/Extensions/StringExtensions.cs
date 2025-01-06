using System.Text.RegularExpressions;
using Test.Fixtures.Utils.Converters.Factory;

namespace Test.Fixtures.Utils.Extensions
{
    public static class StringExtensions
    {
        private const string NA = "(N/A)";
        private const string NULL = "NULL";

        internal static bool IsFitnesseValueNull(this string value)
        {
            return string.IsNullOrEmpty(value)
                || value.Equals(NA, StringComparison.OrdinalIgnoreCase)
                || value.Equals(NULL, StringComparison.OrdinalIgnoreCase);
        }

        internal static async Task<T> ConvertFromFitnesseValue<T>(this string value)
        {
            var typeTConverterClass = ConverterManager.GetConverter<T>();
            var convertedValue = await typeTConverterClass.Convert(value);

            return (T)convertedValue;
        }

        internal static string ToColumnCase(this string propertyName)
        {
            return Regex.Replace(propertyName.Replace(" ", string.Empty), "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        internal static string ToPropertyCase(this string columnName)
        {
            return Regex.Replace(columnName, "(?:[^a-z0-9]|(?<=['\"])s)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        }
    }
}
