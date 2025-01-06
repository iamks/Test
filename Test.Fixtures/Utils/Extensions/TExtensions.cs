using Newtonsoft.Json.Linq;

namespace Test.Fixtures.Utils.Extensions
{
    public static class TExtensions
    {
        public static bool IsEqualTo<T>(this T expected, T actual, bool ignoreElementsOrder = true)
        {
            if(expected == null && (actual == null || actual is Empty))
            {
                return true;
            }

            var actualJson = actual.Serialize<T>(false);
            var expectedJson = expected.Serialize<T>(false);

            Func<string, string, bool> defaultStringCompareFn = (json1, json2) => json1.Equals(json2, StringComparison.InvariantCulture);

            if (ignoreElementsOrder && !defaultStringCompareFn(actualJson, expectedJson))
            {
                actualJson = Normalize(actualJson);
                expectedJson = Normalize(expectedJson);
            }

            return defaultStringCompareFn(actualJson, expectedJson);       
        }

        private static string Normalize(string jsonString)
        {
            return JToken.Parse(jsonString).NormalizeJToken().Serialize<JToken>(false);
        }
    }
}
