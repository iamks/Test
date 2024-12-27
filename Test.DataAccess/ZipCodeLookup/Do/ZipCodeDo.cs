using Newtonsoft.Json;

namespace Test.DataAccess.ZipCodeLookup.Do
{
    public class ZipCodeDo
    {
        [JsonProperty("post code")]
        public string PostCode { get; set; }

        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        public List<PlaceDo> Places { get; set; }
    }
}
