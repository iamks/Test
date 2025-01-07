using Newtonsoft.Json;
using System.Net;
using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.Rest;

namespace Test.Fixtures.Utils.ExecutorBases
{
    public abstract class RestExecutorBase<TRequest, TResponse> : DecisionTableBase
    {
        protected abstract string BaseUri { get; }

        protected abstract string HttpMethod { get; }

        public string Inputs { private get; set; }

        public string Outputs { private get; set; }

        public string EndPoint { private get; set; }

        public string HttpResponseStatus { private get; set; }

        public override async Task Execute()
        {
            await base.Execute();
            RestCall restCall = new RestCall();
            string requestBody = string.Empty;
            if (typeof(TRequest) != typeof(Empty))
            {
                var requestContent = await Inputs.ConvertFromFitnesseValue<TRequest>();
                requestBody = requestContent.Serialize(false);
            }

            if (typeof(TResponse) != typeof(Empty))
            {
                restCall.Response.Outputs = Outputs;
                restCall.Response.HttpResponseStatus = await HttpResponseStatus.ConvertFromFitnesseValue<HttpStatusCode>();
                restCall.Response.ResponseData = await Outputs.ConvertFromFitnesseValue<TResponse>();
            }

            restCall.Request = new RestRequest(BaseUri, requestBody, HttpMethod, EndPoint);
            restCall.Request.Inputs = Inputs;

            //TODO: Add to RestCallManager
            await AddRestCallToEntriesManager(restCall);
        }

        private async Task AddRestCallToEntriesManager(RestCall restCall)
        {
            // Check if the Rest Request already exists in Entries manager
            var restCallEntries = DecisionTableEntriesManager.GetAllEntries<RestCall>();
            var isRestEntryAlreadyExists = restCallEntries.Any(d => d.Request.Equals(restCall.Request));

            if (isRestEntryAlreadyExists)
            {
                throw new Exception($"The rest call is already defined for request: {restCall.Request}");
            }

            DecisionTableEntriesManager.AddEntry(Guid.NewGuid().ToString(), restCall);
        }
    }
}
