using System.Net;

namespace Test.Fixtures.Utils.Rest
{
    public class RestResponse
    {
        public RestResponse() => this.HttpResponseStatus = HttpStatusCode.OK;

        public object ResponseData { get; set; }

        public string Outputs { get; set; }

        public HttpStatusCode HttpResponseStatus { get; set; }
    }
}
