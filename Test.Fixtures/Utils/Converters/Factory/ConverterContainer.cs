using System.Collections.Concurrent;
using Test.Fixtures.Utils.Converters.Contract;

namespace Test.Fixtures.Utils.Converters.Factory
{
    public static class ConverterContainer
    {
        private static readonly ConcurrentBag<IConverter> fitnesseValueConverters = [];

        public static void Register(IConverter? converter)
        {
            if(converter == null)
            {
                return;
            }

            var fitnesseValueConverter = Resolve(converter.Type);
            if (fitnesseValueConverter == null)
            {
                fitnesseValueConverters.Add(converter);
            }
        }

        public static IConverter? Resolve(Type? objectType)
        {
            return fitnesseValueConverters.SingleOrDefault(x => x.Type == objectType);
        }

        public static async void Reset()
        {
            fitnesseValueConverters.Clear();
        }

    }
}
