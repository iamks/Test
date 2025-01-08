using Newtonsoft.Json.Linq;
using Test.Fixtures.Utils.ExecutorBases;

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
            
            var doesCommonPropertiesHaveSameValues = CompareCommonProperties(actualJson, expectedJson);

            if (ignoreElementsOrder && !doesCommonPropertiesHaveSameValues)
            {
                actualJson = Normalize(actualJson);
                expectedJson = Normalize(expectedJson);
            }

            return CompareCommonProperties(actualJson, expectedJson);       
        }

        private static bool CompareCommonProperties(string actualJson, string expectedJson)
        {
            var actualJToken = JToken.Parse(actualJson);
            var expectedJToken = JToken.Parse(expectedJson);

            return actualJToken.CompareCommonProperties(expectedJToken);
        }

        private static string Normalize(string jsonString)
        {
            return JToken.Parse(jsonString).NormalizeJToken().Serialize<JToken>(false);
        }
    }
}
