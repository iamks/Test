using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.Fixtures.Utils.Rest
{
    public class RestRequest : IEquatable<RestRequest>
    {
        public RestRequest(string baseUri, string? requestBody, string httpMethod, string endPoint)
        {
            this.BaseUri = baseUri;
            this.RequestBody = requestBody;
            this.HttpMethod = httpMethod;
            this.EndPoint = endPoint;
        }

        public string BaseUri { get; }
        
        public string EndPoint { get; }

        public string HttpMethod { get; }

        public string? RequestBody { get; }

        public string Inputs { get; internal set; }

        public override string ToString()
        {
            var requestInfo = $"HttpMethod: {this.HttpMethod}\nUri: {this.BaseUri}/{this.EndPoint}";
            if (!string.IsNullOrEmpty(this.RequestBody))
            {
                requestInfo += $"\nBody: {this.RequestBody}";
            }

            return requestInfo;
        }

        public bool Equals(RestRequest? other)
        {
            return other != null
                && this.IsBaseUriEqual(other.BaseUri)
                && this.IsEndpointEqual(other.EndPoint)
                && this.IsHttpMethodEqual(other.HttpMethod)
                && this.IsRequestBodyEqual(other.RequestBody);
        }

        private bool IsBaseUriEqual(string baseUri)
        {
            return this.BaseUri.TrimEnd('/').Equals(baseUri, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsEndpointEqual(string endPoint)
        {
            return this.EndPoint.Equals(endPoint, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsHttpMethodEqual(string httpMethod)
        {
            return this.HttpMethod.Equals(httpMethod, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsRequestBodyEqual(string requestBody)
        {
            if(string.IsNullOrEmpty(this.RequestBody) && string.IsNullOrEmpty(requestBody))
            {
                return true;
            }

            if(string.IsNullOrEmpty(this.RequestBody) || string.IsNullOrEmpty(requestBody))
            {
                return false;
            }

            var currentJObject = JObject.Parse(this.RequestBody);
            var otherJObject = JObject.Parse(requestBody);

            var normalizedCurrentJObject = JsonConvert.SerializeObject(currentJObject, Formatting.None);
            var normalizedOtherJObject = JsonConvert.SerializeObject(otherJObject, Formatting.None);

            return normalizedCurrentJObject == normalizedOtherJObject;
        }

    }
}
