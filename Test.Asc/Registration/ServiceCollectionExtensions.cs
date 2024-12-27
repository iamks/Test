using Microsoft.Extensions.DependencyInjection;
using Test.Asc.Contract.PostalIndex;
using Test.Asc.PostalIndex;

namespace Test.Asc.Registration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAscServices(this IServiceCollection services)
        {
            services.AddScoped<IPostalIndexService, PostalIndexService>();

            return services;
        }
    }
}
