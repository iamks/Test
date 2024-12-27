using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils
{
    public abstract class ExecutorBase<TCapabilityClass, TInput, TOutput> : DecisionTableBase where TCapabilityClass : class
    {
        protected abstract TOutput ExecuteCapability(TCapabilityClass capability, TInput input);

        private const string NA = "(N/A)";

        private TCapabilityClass capability;

        public string Inputs { private get; set; }

        public string ExpectedOutputs { get; private set; }

        public bool StrictOrderComparison { get; set; }

        public string ExpectedErrorMessage { get; private set; }

        public override async Task Execute()
        {
            await base.Execute();
            ExpectedOutputs = NA;
            ExpectedErrorMessage = NA;
            try
            {
                capability = await FitnesseServiceRegistratorFactory.ServiceRegistrator.GetRequiredService<TCapabilityClass>();
                var input = await this.Inputs.ConvertFromFitnesseValue<TInput>();

                TOutput actual = this.ExecuteCapability(capability, input);
                Console.WriteLine("Execute of Executor Base");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ExecutorBase: {0}\nStack trace:{1}", ex);
            }
        }

        public override async Task Reset()
        {
            await base.Reset();
            await FitnesseServiceRegistratorFactory.ServiceRegistrator.Release<TCapabilityClass>(capability);

            capability = default;
        }

    }
}
