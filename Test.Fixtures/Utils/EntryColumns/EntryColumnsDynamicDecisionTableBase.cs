using Test.Fixtures.Utils.DecisionTable;

namespace Test.Fixtures.Utils.EntryColumns
{
    public class EntryColumnsDynamicDecisionTableBase<T> : DynamicDecisionTableBase<T>
        where T : new()
    {
        private readonly List<string> columnNames = new List<string>();
        private readonly string[] exceptionColumnNames = { DEBUG, FITNESSE_ID, "TEMPLATE" };

        public override async Task Set(string propertyName, string propertyValue)
        {
            await base.Set(propertyName, propertyValue);
            var formattedPropertyName = propertyName.Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper();
            if (!exceptionColumnNames.Contains(formattedPropertyName))
            {
                columnNames.Add(formattedPropertyName);
            }
        }

        public override async Task Execute()
        {
            await base.Execute();
            EntryColumnsManager.AddEntry(Entry, columnNames);
        }

        public override async Task Reset()
        {
            await base.Reset();
            columnNames.Clear();
        }
    }
}