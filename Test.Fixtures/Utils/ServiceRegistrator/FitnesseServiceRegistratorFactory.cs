namespace Test.Fixtures.Utils.ServiceRegistrator
{
    public sealed class FitnesseServiceRegistratorFactory
    {
        private static IFitnesseServiceRegistrator serviceRegistrator;

        public static IFitnesseServiceRegistrator ServiceRegistrator
        {
            get
            {
                return serviceRegistrator
                    ?? throw new Exception("Fitnesse Service Factory manager is not created");
            }
            private set
            {
                serviceRegistrator = value;
            }
        }

        internal static void Set(IFitnesseServiceRegistrator serviceRegistratorFactory)
        {
            serviceRegistrator = serviceRegistratorFactory;
        }
    }
}
