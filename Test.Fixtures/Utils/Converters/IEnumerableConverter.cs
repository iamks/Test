using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Converters.Factory;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.Converters
{
    public class IEnumerableConverter<TItem> : IConverter
    {
        private const char DELIMITER = ';';

        private const string NULL = "NULL";

        public virtual Type Type => typeof(IEnumerableConverter<TItem>);

        public virtual async Task<object?> Convert(string fitnesseValue)
        {
            if (fitnesseValue.Equals(NULL, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (fitnesseValue.IsFitnesseValueNull())
            {
                return new List<TItem>();
            }

            var stringList = fitnesseValue.Split(DELIMITER).Select(val => val.Trim()).ToList();
            
            var typeTItemConverterClass = ConverterManager.GetConverter<TItem>();
            var convertTasks = stringList.Select(str => typeTItemConverterClass.Convert(str));
            await Task.WhenAll(convertTasks);

            return convertTasks.Select(task => (TItem)task.Result).ToList<TItem>();
        }
    }
}
