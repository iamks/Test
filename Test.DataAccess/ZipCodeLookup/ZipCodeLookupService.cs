using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Test.DataAccess.ZipCodeLookup.Do;

namespace Test.DataAccess.ZipCodeLookup
{
    public class ZipCodeLookupService : IZipCodeLookupService
    {
        private const string ZipCodeLookUp_GetZipCodeDetails_Url = "{0}/{1}";
        private readonly string? ZipCodeLookUp_BaseUrl;


        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public ZipCodeLookupService(
            IConfiguration configuration, 
            IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            ZipCodeLookUp_BaseUrl = this.configuration.GetSection("ZipCodeLookUp:BaseUrl")?.Value;
        }

        public async Task<ZipCodeDo?> GetZipCodeDetails(string zipCode)
        {
            var response = new ZipCodeDo();

            var url = string.Format(ZipCodeLookUp_GetZipCodeDetails_Url, ZipCodeLookUp_BaseUrl, zipCode);

            using HttpClient httpClient = this.httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(responseContent))
            {
                response = JsonConvert.DeserializeObject<ZipCodeDo>(responseContent);
            }

            return response;
        }
    }
}
