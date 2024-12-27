using Newtonsoft.Json;
using System.Net;
using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.Rest;

namespace Test.Fixtures.Utils
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
                var requestContent = await this.Inputs.ConvertFromFitnesseValue<TRequest>();
                requestBody = JsonConvert.SerializeObject(requestContent, Formatting.None);
            }

            if (typeof(TResponse) != typeof(Empty))
            {
                restCall.Response.Outputs = this.Outputs;
                restCall.Response.HttpResponseStatus = await this.HttpResponseStatus.ConvertFromFitnesseValue<HttpStatusCode>();
                restCall.Response.ResponseData = await this.Outputs.ConvertFromFitnesseValue<TResponse>();
            }

            restCall.Request = new RestRequest(this.BaseUri, requestBody, this.HttpMethod, this.EndPoint);
            restCall.Request.Inputs = this.Inputs;

            //TODO: Add to RestCallManager
            await this.AddRestCallToEntriesManager(restCall);
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

            DecisionTableEntriesManager.AddEntry<RestCall>(Guid.NewGuid().ToString(), restCall);
        }
    }
}
