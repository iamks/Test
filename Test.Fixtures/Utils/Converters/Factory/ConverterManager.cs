using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils.Converters.Factory
{
    public static class ConverterManager
    {
        public static async Task<IConverter?> GetConverter<T>()
        {
            IConverter? converterInstance = null;

            var propertyType = Nullable.GetUnderlyingType(typeof(T)) != null //It is nullable type (e.g., Nullable<int> or int?)
                ? Nullable.GetUnderlyingType(typeof(T))
                : typeof(T);

            var preRegisteredConverters = await FitnesseServiceRegistratorFactory.ServiceRegistrator.GetServices<IConverter>();
            converterInstance = preRegisteredConverters.SingleOrDefault(x => x.Type == propertyType);
            
            if (converterInstance != null)
            {
                return converterInstance;
            }

            converterInstance = ConverterContainer.Resolve(propertyType);
            if (converterInstance != null)
            {
                return converterInstance;
            }

            if (propertyType.IsEnum)
            {
                return await CreateAndRegisterGenericConverterInstance(typeof(EnumConverter<>), propertyType);
            }
            else if (propertyType.IsGenericType && propertyType.IsObjectOfType(typeof(IEnumerable<>)))
            {
                var typeIEnumerableConverter = typeof(IEnumerableConverter<>);
                var typeTItem = propertyType.GenericTypeArguments[0];

                return await CreateAndRegisterGenericConverterInstance(typeIEnumerableConverter, typeTItem);
            }
            else if(propertyType.IsGenericType && propertyType.IsObjectOfType(typeof(ICollection<>)))
            {
                var typeCollectionConverter = typeof(CollectionConverter<>);
                var typeTItem = propertyType.GenericTypeArguments[0];

                return await CreateAndRegisterGenericConverterInstance(typeCollectionConverter, typeTItem);
            }

            return await CreateAndRegisterGenericConverterInstance(typeof(FitnesseIdEntryConverter<>), propertyType);
        }

        private static async Task<IConverter?> CreateAndRegisterGenericConverterInstance(Type converterType, Type genericType)
        {
            var converterInstance = Activator.CreateInstance(converterType.MakeGenericType(genericType));
            var iConverterInstance = converterInstance as IConverter;

            ConverterContainer.Register(iConverterInstance);

            return iConverterInstance;
        }

    }
}
