using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils.ExecutorBases
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

        public string ExpectedErrorMessage { get; set; }

        public override async Task Execute()
        {
            await base.Execute();
            ExpectedOutputs = NA;
            ExpectedErrorMessage = NA;
            try
            {
                capability = await FitnesseServiceRegistratorFactory.ServiceRegistrator.GetRequiredService<TCapabilityClass>();
                var input = await Inputs.ConvertFromFitnesseValue<TInput>();

                TOutput actual = ExecuteCapability(capability, input);

                var expectedOutputFitnesseId = expectedOutputs[index];
                TOutput expected = await GetExpectedResponse(expectedOutputFitnesseId);

                var isOutputsEqual = await CompareOutputs(expected, actual);
                if (isOutputsEqual)
                {
                    ExpectedOutputs = expectedOutputs[index];
                }
                else
                {
                    var actualJson = actual.Serialize();
                    var expectedJson = expected.Serialize();

                    ExpectedOutputs = $"Expected output does not match the actual response:\r\nExpected:\r\n{expectedJson}\r\nActual:\r\n{actualJson}";
                }
            }
            catch (Exception ex)
            {
                ExpectedErrorMessage = ex.Message;
                Console.WriteLine("Exception in ExecutorBase: {0}\nStack trace:{1}", ex);
            }
        }

        public override async Task Reset()
        {
            index++;

            await base.Reset();
            capability = default;
        }

        public override async Task Table(List<List<string>> table)
        {
            await base.Table(table);

            index = -1;
            expectedOutputs = new string[table.Count - 1];

            var expectedOutputsColumn = $"{ExpectedOutputsColumn.ToColumnCase()}?";
            var firstTable = table[0];
            int expectedOutputsColumnIndex = firstTable.Contains(expectedOutputsColumn)
                ? firstTable.IndexOf(expectedOutputsColumn)
                : throw new Exception("Missing column '" + expectedOutputsColumn + "'");

            for (int i = 1; i < table.Count; ++i)
            {
                List<string> tableRows = table[i];
                expectedOutputs[i - 1] = tableRows[expectedOutputsColumnIndex];
            }
        }

        protected virtual async Task<TOutput> GetExpectedResponse(string expectedOutputFitnesseId)
        {
            return await expectedOutputFitnesseId.ConvertFromFitnesseValue<TOutput>();
        }

        protected virtual async Task<bool> CompareOutputs(TOutput expectedOutput, TOutput actualOutput)
        {
            var ignoreElementsOrder = StrictOrderComparison ? false : true;

            return expectedOutput.IsEqualTo(actualOutput, ignoreElementsOrder);
        }
    }
}
