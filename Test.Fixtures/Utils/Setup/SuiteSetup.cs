using Test.Fixtures.Utils.DecisionTable;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils.Setup
{
    public class SuiteSetup : DecisionTableBase
    {
        private const string OK = "OK";
        private readonly IFitnesseServiceRegistrator fitnesseServiceRegistrator;

        public SuiteSetup()
        {
            fitnesseServiceRegistrator = new FitnesseServiceRegistrator();
        }

        public string Setup()
        {
            try
            {
                FitnesseServiceRegistratorFactory.Set(fitnesseServiceRegistrator);

                DatabaseSetup.InitializeDbContexts();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred during Fitnesse Setup: \n{0}\nStack trace:\n{1}", ex, ex.StackTrace);
            }

            return OK;
        }
    }
}
