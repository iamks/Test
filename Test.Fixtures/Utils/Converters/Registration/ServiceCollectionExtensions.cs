using Microsoft.Extensions.DependencyInjection;
using Test.Fixtures.Utils.Converters.Contract;

namespace Test.Fixtures.Utils.Converters.Registration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFitnesseConvertersServices(this IServiceCollection services)
        {
            //services.AddSingleton<ConverterFactory>();

            // Add Fitnesse value converters
            services.AddSingleton<IConverter, StringConverter>();
            services.AddSingleton<IConverter, BoolConverter>();
            services.AddSingleton<IConverter, IntConverter>();
            services.AddSingleton<IConverter, DoubleConverter>();
            services.AddSingleton<IConverter, FloatConverter>();
            services.AddSingleton<IConverter, DecimalConverter>();
            services.AddSingleton<IConverter, GuidConverter>();
            services.AddSingleton<IConverter, DateTimeConverter>();
            services.AddSingleton<IConverter, DateTimeOffsetConverter>();

            return services;
        }
    }
}
