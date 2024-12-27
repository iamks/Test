using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Esc.PostalIndex.DataAccess.Context;

namespace Test.Esc.PostalIndex.DataAccess.Registration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostalIndexDataAccessServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            if(configuration != null)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<PostalIndexDbContext>(option =>
                {
                    option.UseSqlServer(connectionString);
                });
            }

            services.AddScoped<IPostalIndexRepository, PostalIndexRepository>();

            return services;
        }
    }
}
