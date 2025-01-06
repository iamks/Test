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

    }
}
