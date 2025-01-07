using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.Fixtures.Utils.Extensions
{
    public static class JsonExtensions
    {
        public static TOutput? Deserialize<TOutput>(this string output)
        {
            return JsonConvert.DeserializeObject<TOutput>(output);
        }

        public static string Serialize<TInput>(this TInput input, bool isIndented = true)
        {
            JsonSerializerSettings settings = new()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            };

            var formatting = isIndented ? Formatting.Indented : Formatting.None;

            return JsonConvert.SerializeObject(input, formatting, settings);
        }

        public static JToken NormalizeJToken(this JToken jToken)
        {
            if(jToken.Type == JTokenType.Object)
            {
                // Sort the properties of the JObject alphabetically by property name
                var jObject = (JObject)jToken;
                var jProperties = jObject.Properties().ToList();
                jProperties.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.InvariantCulture));

                // Create a new JObject with sorted properties
                JObject normalizedJObject = new JObject();
                foreach (JProperty jproperty in jProperties)
                {
                    normalizedJObject.Add(jproperty.Name, jproperty.Value.NormalizeJToken()); // Recurse for values
                }

                return normalizedJObject;
            }
            else if(jToken.Type == JTokenType.Array)
            {
                var jArray = (JArray)jToken;

                // Sort the elements of the JArray based on their string representation
                for (int index = 0; index < jArray.Count; ++index)
                {
                    jArray[index] = jArray[index].NormalizeJToken(); // Recurse for each element
                }
            }

            // For other token types (like JValue, JConstructor, etc.), return the token as is
            return jToken;
        }

        public static bool CompareJSonTokensCommonProperties(this JToken token1, JToken token2)
        {
            if (token1.Type != token2.Type)
            {
                return false; // If types are different, return false
            }

            switch (token1.Type)
            {
                case JTokenType.Object:
                    return CompareJsonObjects((JObject)token1, (JObject)token2);
                case JTokenType.Array:
                    return CompareJsonArrays((JArray)token1, (JArray)token2);
                default:
                    return JToken.DeepEquals(token1, token2); // For other types (string, number, etc.), just compare
            }
        }

        private static bool CompareJsonObjects(JObject jObject1, JObject jObject2)
        {
            // Get the common properties between the two JObject instances
            var commonProperties = jObject1.Properties()
                                           .Where(p => jObject2.ContainsKey(p.Name))
                                           .ToList();

            // Compare the values of the common properties at the top level
            foreach (var property in commonProperties)
            {
                var value1 = property.Value;
                var value2 = jObject2[property.Name];

                // Recursively compare values of common properties
                if (!CompareJSonTokensCommonProperties(value1, value2))
                {
                    return false; // If any common property values are different, return false
                }
            }

            return true; // All common properties are equal
        }

        private static bool CompareJsonArrays(JArray array1, JArray array2)
        {
            // If arrays have different lengths, return false
            if (array1.Count != array2.Count)
            {
                return false;
            }

            // Sort the arrays to ignore order, if necessary
            var sortedArray1 = array1.OrderBy(x => x.ToString()).ToList();
            var sortedArray2 = array2.OrderBy(x => x.ToString()).ToList();

            // Compare each element in the arrays
            for (int i = 0; i < sortedArray1.Count; i++)
            {
                if (!CompareJSonTokensCommonProperties(sortedArray1[i], sortedArray2[i]))
                {
                    return false; // If any elements are different, return false
                }
            }

            return true; // All elements in the arrays are equal
        }
    }
}
