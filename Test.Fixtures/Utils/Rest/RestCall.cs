namespace Test.Fixtures.Utils.Rest
{
    public class RestCall
    {
        public RestCall()
        {
            var emptyString = string.Empty;
            this.Request = new RestRequest(emptyString, emptyString, emptyString, emptyString);
            this.Response = new RestResponse();
        }

        public RestRequest Request { get; set; }

        public RestResponse Response { get; }
    }
}
