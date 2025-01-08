using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;

namespace Test.Fixtures.Utils.ExecutorBases
{
    public abstract class ControllerExecutorBase<TController, TInput, TOutput> : ExecutorBase<TController, TInput, TOutput>
        where TController : class
    {
        private readonly Func<TController, TInput, Task<HttpResponseMessage>> func;

        public string ExpectedHttpStatusCode { get; private set; }

        protected abstract Expression<Func<TController, TInput, Task<HttpResponseMessage>>> MethodAsync { get; }

        protected abstract Expression<Func<TController, TInput, HttpResponseMessage>> Method { get; }

        protected virtual TimeSpan Timeout { get; } = TimeSpan.FromSeconds(15);

        protected ControllerExecutorBase()
        {
            if (MethodAsync != null)
            {
                func = MethodAsync.Compile();

                var mce = this.MethodAsync.Body as MethodCallExpression;
                var authorizedAttribute =
                    mce.Method.CustomAttributes.SingleOrDefault(a => a.AttributeType == typeof(AuthorizeAttribute));

                if (authorizedAttribute != null)
                {
                    // Inspect the roles or permissions from the attribute
                    var roles = authorizedAttribute.ConstructorArguments
                                                  .FirstOrDefault(arg => arg.ArgumentType == typeof(string)).Value?.ToString();

                    // TODO: Check if the current user has the required roles, return UnAuthoized if not; else proceed
                }
            }
            else
            {
                func = (controller, input) => Task.FromResult(Method.Compile().Invoke(controller, input));
            }
        }

        protected override TOutput ExecuteCapability(TController controller, TInput input)
        {
            try
            {
                var executeCallTask = Task.Run(async () => await ExecuteCall(controller, input));
                executeCallTask.Wait();

                var httpResponseMessage = executeCallTask.Result;
                ExpectedHttpStatusCode = ((int)httpResponseMessage.StatusCode).ToString();

                TOutput? output = default;

                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var content = GetContent(httpResponseMessage);
                    if (!string.IsNullOrEmpty(content))
                    {
                        output = JsonConvert.DeserializeObject<TOutput>(content);
                    }
                }
                else if (httpResponseMessage.Content != null)
                {
                    var content = GetContent(httpResponseMessage);
                    ExpectedErrorMessage = content;
                }
             
                return output;
            }
            catch (Exception ex)
            {
                ExpectedHttpStatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                ExpectedErrorMessage = ex.Message;

                throw;
            }
        }

        private string GetContent(HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        protected Task<HttpResponseMessage> InvokeFuncAsync(TController controller, TInput input)
        {
            return func(controller, input);
        }

        private async Task<HttpResponseMessage> ExecuteCall(TController controller, TInput input)
        {
            var callFuncTask = InvokeFuncAsync(controller, input);
            if (!Debugger.IsAttached)
            {
                var timeoutTask = Task.Delay(Timeout);
                // Wait for either the task to complete or the timeout to occur.
                var taskCompletedFirst = await Task.WhenAny(callFuncTask, timeoutTask);
                if (taskCompletedFirst == timeoutTask)
                {
                    // Timeout occurred
                    throw new TimeoutException($"The operation timed out after ${Timeout.TotalSeconds}s");
                }
            }

            return await callFuncTask;
        }
    }
}
