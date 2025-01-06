using System.Collections.ObjectModel;
using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Converters.Factory;

namespace Test.Fixtures.Utils.Converters
{
    public class CollectionConverter<TItem> : IConverter
    {
        public Type Type => typeof(ICollection<TItem>);

        public async Task<object?> Convert(string fitnesseValue)
        {
            var iEnumerableConverter = ConverterManager.GetConverter<IEnumerable<TItem>>();
            var iEnumerables = await iEnumerableConverter.Convert(fitnesseValue);

            if(iEnumerables == null)
            {
                return null;
            } 

            return new Collection<TItem>(((IEnumerable<TItem>)iEnumerables).ToList());
        }
    }
}
