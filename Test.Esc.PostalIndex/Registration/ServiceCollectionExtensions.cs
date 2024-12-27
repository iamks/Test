using Microsoft.Extensions.DependencyInjection;
using Test.Esc.Contract.PostalIndex;

namespace Test.Esc.PostalIndex.Registration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostalIndexServices(this IServiceCollection services)
        {
            services.AddScoped<IPostalIndexService, PostalIndexService>();

            return services;
        }
    }
}
