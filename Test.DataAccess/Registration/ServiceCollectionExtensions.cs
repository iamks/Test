using Microsoft.Extensions.DependencyInjection;
using Test.DataAccess.ZipCodeLookup;

namespace Test.DataAccess.Registration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            // Register ZipCodeLookUp services
            services.AddScoped<IZipCodeLookupService, ZipCodeLookupService>();

            return services;
        }
    }
}
