using Test.Fixtures.Utils.DecisionTable;

namespace Test.Fixtures.Utils.Rest
{
    public static class RestCallManager
    {
        public static async Task<RestResponse> Get(RestRequest request)
        {
            var restCallEntries = DecisionTableEntriesManager.GetAllEntries<RestCall>();
            if(!restCallEntries.Any(d => d.Request.Equals(request)))
            {
                throw new Exception($"No configuration found for the following REST request:\n {request}");
            }

            var restCall = restCallEntries.First(call => call.Request.Equals(request));
            
            return restCall.Response;
        }
    }
}
