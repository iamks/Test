using Test.Fixtures.Utils.Converters.Contract;

namespace Test.Fixtures.Utils.DecisionTable
{
    public class ColumnMap<TDto>
    {
        public ColumnMap(
            string columnName,
            IConverter converter,
            Action<TDto, object> SetPropertyValueFn)
        {
            this.ColumnName = columnName;
            this.Converter = converter;
            this.SetPropertyValueFn = SetPropertyValueFn;
        }

        public string ColumnName { get; }

        public IConverter Converter { get; }

        public Action<TDto, object> SetPropertyValueFn { get; }
    }
}
