using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Extensions;
using Test.Fixtures.Utils.ServiceRegistrator;

namespace Test.Fixtures.Utils.Converters.Factory
{
    public static class ConverterManager
    {
        public static IConverter? GetConverter<T>()
        {
            IConverter? converterInstance = null;

            var propertyType = Nullable.GetUnderlyingType(typeof(T)) != null //It is nullable type (e.g., Nullable<int> or int?)
                ? Nullable.GetUnderlyingType(typeof(T))
                : typeof(T);

            var preRegisteredConverters = FitnesseServiceRegistratorFactory.ServiceRegistrator.GetServices<IConverter>().GetAwaiter().GetResult();
            
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
                return CreateAndRegisterGenericConverterInstance(typeof(EnumConverter<>), propertyType);
            }
            else if (propertyType.IsGenericType && propertyType.IsObjectOfType(typeof(IEnumerable<>)))
            {
                var typeIEnumerableConverter = typeof(IEnumerableConverter<>);
                var typeTItem = propertyType.GenericTypeArguments[0];

                return CreateAndRegisterGenericConverterInstance(typeIEnumerableConverter, typeTItem);
            }
            else if(propertyType.IsGenericType && propertyType.IsObjectOfType(typeof(ICollection<>)))
            {
                var typeCollectionConverter = typeof(CollectionConverter<>);
                var typeTItem = propertyType.GenericTypeArguments[0];

                return CreateAndRegisterGenericConverterInstance(typeCollectionConverter, typeTItem);
            }

            return CreateAndRegisterGenericConverterInstance(typeof(FitnesseIdEntryConverter<>), propertyType);
        }

        private static IConverter? CreateAndRegisterGenericConverterInstance(Type converterType, Type genericType)
        {
            var converterInstance = Activator.CreateInstance(converterType.MakeGenericType(genericType));
            var iConverterInstance = converterInstance as IConverter;

            ConverterContainer.Register(iConverterInstance);

            return iConverterInstance;
        }

    }
}
