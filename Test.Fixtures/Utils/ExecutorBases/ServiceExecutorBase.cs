using System.Diagnostics;
using System.Linq.Expressions;

namespace Test.Fixtures.Utils.ExecutorBases
{
    public abstract class ServiceExecutorBase<TService, TInput, TOutput> : ExecutorBase<TService, TInput, TOutput>
       where TService : class
    {
        private readonly Func<TService, TInput, Task<TOutput>> func;

        protected abstract Expression<Func<TService, TInput, Task<TOutput>>> MethodAsync { get; }

        protected abstract Expression<Func<TService, TInput, TOutput>> Method { get; }

        protected virtual TimeSpan Timeout { get; } = TimeSpan.FromSeconds(15);

        protected ServiceExecutorBase()
        {
            if (MethodAsync != null)
            {
                func = MethodAsync.Compile();
            }
            else
            {
                func = (service, input) => Task.FromResult(Method.Compile().Invoke(service, input));
            }
        }

        protected override TOutput ExecuteCapability(TService service, TInput input)
        {
            var executeCallTask = Task.Run(async () => await ExecuteCall(service, input));
            executeCallTask.Wait();

            return executeCallTask.Result;
        }

        protected Task<TOutput> InvokeFuncAsync(TService service, TInput input)
        {
            return func(service, input);
        }

        private async Task<TOutput> ExecuteCall(TService service, TInput input)
        {
            var callFuncTask = InvokeFuncAsync(service, input);
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
