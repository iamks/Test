using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Dependency;
using Test.Fixtures.Mock.Rest;
using Test.Fixtures.Utils.Converters.Registration;

namespace Test.Fixtures.Utils.ServiceRegistrator
{
    public class FitnesseServiceRegistrator : IFitnesseServiceRegistrator
    {
        private const string AppSettings_Json = "appSettings.json";

        private readonly IServiceProvider _serviceProvider;

        public FitnesseServiceRegistrator()
        {
            var services = new ServiceCollection();

            // Register IConfiguration for the Fitnesse
            this.AggregateIConfigurationService(services);

            // Register custom Rest Handler and HttpClient
            this.AggregateHttpServices(services);

            // Register all the services to Fitnesse Service Registrator container
            services.AddTestApiServices();
            services.AddFitnesseConvertersServices();
                
            _serviceProvider = services.BuildServiceProvider();
        }

        public async Task<T> GetRequiredService<T>() where T: notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public async Task<IEnumerable<T>> GetServices<T>() where T: notnull
        {
            return _serviceProvider.GetServices<T>();
        }

        public async Task Release<T>(T service)
        {
            if(service is IDisposable)
            {
                ((IDisposable)service).Dispose();
            }
        }

        #region PrivateMethods

        private void AggregateIConfigurationService(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(AppSettings_Json, optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
        }

        private void AggregateHttpServices(ServiceCollection services)
        {
            services.AddTransient<MockHttpMessageHandler>();
            services.AddHttpClient().ConfigureHttpClientDefaults(client => 
            {
                client.ConfigurePrimaryHttpMessageHandler(() => new MockHttpMessageHandler());
            });
        }

        #endregion
    }
}
