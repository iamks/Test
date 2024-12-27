using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Test.Asc.Registration;
using Test.DataAccess.Registration;
using Test.Esc.PostalIndex.DataAccess.Registration;
using Test.Esc.PostalIndex.Registration;

namespace Test.Dependency
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddTestApiServices(this IServiceCollection services)
        {
            //ASC
            services.AddAscServices();

            //DataAccess
            services.AddDataAccessServices();

            //ESC
            services.AddPostalIndexServices();
            services.AddPostalIndexDataAccessServices();

            //Register AutoMapper Profile
            services.AddAutoMapper(conf => RegisterProfiles(conf));

            return services;
        }

        public static void RegisterProfiles(IMapperConfigurationExpression configuration)
        {
            //ASC
            //PostalIndex
            configuration.AddProfile(typeof(Asc.PostalIndex.Mapping.PostalIndexProfile));
            configuration.AddProfile(typeof(Asc.WebApi.PostalIndex.Mapping.PostalIndexProfile));

            //ESC
            //PostalIndex
            configuration.AddProfile(typeof(Esc.PostalIndex.Mapping.PostalIndexProfile));
        }

    }
}
