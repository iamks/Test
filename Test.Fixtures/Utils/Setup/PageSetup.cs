using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.EntryColumns;

namespace Test.Fixtures.Utils.Setup
{
    public class PageSetup : DecisionTableBase
    {
        private const string OK = "OK";
        private const string KO = "KO";

        public PageSetup()
        {
            
        }

        public string Setup()
        {
            try
            {
                EntryColumnsManager.ClearData();
                DecisionTableEntriesManager.ClearData();

                DatabaseSetup.ClearData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred when clearing data on PageSetup: \n{ex}");
                
                return KO;
            }

            return OK;
        }
    }
}
