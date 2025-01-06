using Newtonsoft.Json;
using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils
{
    public abstract class ExecutorBase<TCapabilityClass, TInput, TOutput> : DecisionTableBase where TCapabilityClass : class
    {

        private const string NA = "(N/A)";
        private const string ExpectedOutputsColumn = "ExpectedOutputs";

        private TCapabilityClass capability;
        private int index;
        private string[] expectedOutputs;

        protected abstract TOutput ExecuteCapability(TCapabilityClass capability, TInput input);

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
                
                var expectedOutputFitnesseId = this.expectedOutputs[this.index];
                TOutput expected = await this.GetExpectedResponse(expectedOutputFitnesseId);

                var isOutputsEqual = await CompareOutputs(actual, expected);
                if (isOutputsEqual)
                {
                    this.ExpectedOutputs = expectedOutputs[this.index];
                }
                else 
                {
                    var actualJson = actual.Serialize<TOutput>();
                    var expectedJson = expected.Serialize<TOutput>();

                    this.ExpectedOutputs = $"Expected output does not match the actual response:\r\nExpected:\r\n{expectedJson}\r\nActual:\r\n{actualJson}";
                }

                Console.WriteLine("Execute of Executor Base");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ExecutorBase: {0}\nStack trace:{1}", ex);
            }
        }

        public override async Task Reset()
        {
            this.index++;

            await base.Reset();
            await FitnesseServiceRegistratorFactory.ServiceRegistrator.Release<TCapabilityClass>(capability);

            capability = default;
        }

        public override async Task Table(List<List<string>> table)
        {
            await base.Table(table);

            this.index = -1;
            this.expectedOutputs = new string[table.Count - 1];

            var expectedOutputsColumn = $"{ExpectedOutputsColumn.ToColumnCase()}?";
            var firstTable = table[0];
            int expectedOutputsColumnIndex = firstTable.Contains(expectedOutputsColumn)
                ? firstTable.IndexOf(expectedOutputsColumn)
                : throw new Exception("Missing column '" + expectedOutputsColumn + "'");

            for (int i = 1; i < table.Count; ++i)
            {
                List<string> tableRows = table[i];
                this.expectedOutputs[i - 1] = tableRows[expectedOutputsColumnIndex];
            }
        }

        protected virtual async Task<TOutput> GetExpectedResponse(string expectedOutputFitnesseId)
        {
            return await expectedOutputFitnesseId.ConvertFromFitnesseValue<TOutput>();
        }

        protected virtual async Task<bool> CompareOutputs(TOutput expectedOutput, TOutput actualOutput)
        {
            var ignoreElementsOrder = this.StrictOrderComparison ? false : true;
            
            return expectedOutput.IsEqualTo(actualOutput, ignoreElementsOrder);
        }

    }
}
