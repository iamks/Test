using Newtonsoft.Json;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.Rest;

namespace Test.Fixtures.Mock.Rest
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var absoluteUri = new Uri(request.RequestUri.AbsoluteUri);
            var baseUri = absoluteUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped).TrimEnd('/');
            var endPoint = absoluteUri.PathAndQuery.TrimStart('/');
            var httpMethod = request.Method.ToString(); 

            string? requestBody = null;
            if(request.Content != null)
            {
                requestBody = await request.Content.ReadAsStringAsync();
            }
            var restRequest = new RestRequest(baseUri, requestBody, httpMethod, endPoint);
            
            // Fetch mocked response for the request from RestCallManager
            var restResponse = await RestCallManager.Get(restRequest);

            //var responseBody = JsonConvert.SerializeObject(restResponse.ResponseData);
            var responseBody = restResponse.ResponseData.Serialize<object>(false);
            var responseStatus = restResponse.HttpResponseStatus;
            var httpResponseMessage = new HttpResponseMessage(responseStatus)
            {
                Content = new StringContent(responseBody)
            };

            return httpResponseMessage;
        }
    }

}
