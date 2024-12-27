using Microsoft.EntityFrameworkCore;
using Test.Esc.PostalIndex.DataAccess.Context;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils.Setup
{
    public static class DatabaseSetup
    {
        private static readonly List<DbContext> _dbContexts = [];

        private const string Database_Path = "../Binaries/net8.0/win-x64/Database/";

        public static void InitializeDbContexts()
        {
            // Add all the Dbcontexts used in the application here
            var initializePostalIndexDbContext = Task.Run(async () => await InitializePostalIndexDbContext());
            initializePostalIndexDbContext.Wait();
        }

        public static void ClearData()
        {
            // Run ClearData scripts on each DbContext
            var clearDataOnPostalIndexDbContextTask = Task.Run(async () => await ClearDataOnPostalIndexDbContext());
            clearDataOnPostalIndexDbContextTask.Wait();
        }

        #region PostalIndexDbContext

        private static async Task InitializePostalIndexDbContext()
        {
            var postalIndexDbContext = await FitnesseServiceRegistratorFactory.ServiceRegistrator.GetRequiredService<PostalIndexDbContext>();
            _dbContexts.Add(postalIndexDbContext);

            await postalIndexDbContext.Database.EnsureCreatedAsync();
        }

        private static async Task ClearDataOnPostalIndexDbContext()
        {
            var postalIndexClearSqlScript = await File.ReadAllTextAsync($"{Database_Path}/PostalIndex/Scripts/ClearData.sql");
            var postalIndexDbContext = await FitnesseServiceRegistratorFactory.ServiceRegistrator.GetRequiredService<PostalIndexDbContext>();

            await postalIndexDbContext.Database.ExecuteSqlRawAsync(postalIndexClearSqlScript);
        }

        #endregion
    }
}
