using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class EnumConverter<TEnum> : IConverter
    {
        public Type Type => typeof(TEnum);

        public async Task<object?> Convert(string fitnesseValue)
        {
            object? parsedEnumValue = null;
            if (!fitnesseValue.IsFitnesseValueNull())
            {
                try
                {
                    parsedEnumValue = Enum.Parse(typeof(TEnum), fitnesseValue);
                }
                catch
                {
                    var validEnumValues = string.Join(", ", Enum.GetNames(Type));
                    Console.WriteLine($"The value {fitnesseValue} does not exists on Enum {Type.Name}. The Enum contains only the following values: {validEnumValues}");
                }
            }

            return parsedEnumValue;
        }
    }
}
