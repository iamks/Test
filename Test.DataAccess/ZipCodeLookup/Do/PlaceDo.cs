using Newtonsoft.Json;

namespace Test.DataAccess.ZipCodeLookup.Do
{
    public class PlaceDo
    {
        [JsonProperty("place name")]
        public string PlaceName { get; set; }
        public string Longitude { get; set; }
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
        public string Latitude { get; set; }
    }
}
