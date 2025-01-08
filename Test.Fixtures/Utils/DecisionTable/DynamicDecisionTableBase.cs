using System.Reflection;
using Test.Fixtures.Utils.Converters.Contract;
using Test.Fixtures.Utils.Converters.Factory;
using Test.Fixtures.Utils.Extensions;

namespace Test.Fixtures.Utils.DecisionTable
{
    public abstract class DynamicDecisionTableBase<T> : DecisionTableBase where T : new()
    {
        protected DynamicDecisionTableBase()
        {
            this.InitializeProperties();
        }

        //Key - columnName,
        //value - Action<typeT, columnValue> to set column value on property columnName on type T
        private readonly Dictionary<string, Action<T, string>> propertySetterFunctions = new();
        
        private readonly List<ColumnMap<T>> columnMappings = new();

        protected T Entry { get; private set; }

        protected const string DEBUG = "DEBUG";

        protected const string FITNESSE_ID = "FITNESSEID";

        public override async Task Execute()
        {
            await base.Execute();

            DecisionTableEntriesManager.AddEntry(FitnesseId, Entry);
        }

        /// <summary>
        /// As in a Decision Table, all the set method calls are done before the execute call 
        /// </summary>
        /// <param name="headerName">Column Header Name</param>
        /// <param name="value">Column Value</param>
        /// <returns></returns>
        public async virtual Task Set(string headerName, string value)
        {
            var formattedHeaderName = headerName.ToPropertyCase().ToUpper();
            if (formattedHeaderName == DEBUG)
            {
                Debug = await value.ConvertFromFitnesseValue<bool>();
            }
            else if (formattedHeaderName == FITNESSE_ID)
            {
                FitnesseId = value;
            }
            else
            {
                if (!this.propertySetterFunctions.TryGetValue(formattedHeaderName, out Action<T, string> propertySetterFn))
                {
                    throw new Exception($"Mapping  not defined for '{headerName}' on object {typeof(T).FullName}");
                }

                propertySetterFn(this.Entry, value);
            }
        }

        /// <summary>
        /// As in a Decision Table, all the get method calls are done after the execute call.
        /// </summary>
        /// <param name="headerName">Column Header Name</param>
        /// <returns></returns>
        public async virtual Task Get(string headerName)
        {
        }

        public override async Task Reset()
        {
            await base.Reset();
            Entry = new T();
            this.SetDefaultValueOnEntryProperties();
        }

        protected virtual void SetupCustomColumnMappings()
        {
        }

        /// <summary>
        /// Wrapper to add custom property value map if needed to ColumnMappings
        /// </summary>
        /// <typeparam name="TPropertyType">Type of property to be set</typeparam>
        /// <param name="propertyName">column name</param>
        /// <param name="setPropertyFn">Function to set property value on property(column name) on type T</param>
        /// <returns></returns>
        protected virtual void AddColumnMapping<TPropertyType>(
            string propertyName, 
            Action<T, TPropertyType> setPropertyFn)
        {
            var columnName = propertyName;
            var converter = ConverterManager.GetConverter<TPropertyType>();
            var setPropertyValueFn = (Action<T, object>) ((dto, value) => setPropertyFn(dto, (TPropertyType)value));

            var columnMap = new ColumnMap<T>(propertyName, converter, setPropertyValueFn);
            this.columnMappings.Add(columnMap);
        }

        protected virtual void SetDefaultValueOnEntryProperties()
        {
        }

        private void InitializeProperties()
        {
            Dictionary<string, ColumnMap<T>> columnNameWithMapping = new();
            this.AggregateCustomColumnMappingsIfAny(columnNameWithMapping);

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var columnName = property.Name.ToPropertyCase().ToUpper();
                if (!columnNameWithMapping.ContainsKey(columnName))
                {
                    var columnMapping = CreateColumnMapping<T>(property);
                    columnNameWithMapping.Add(columnName, columnMapping);
                }
            }

            foreach (var element in columnNameWithMapping)
            {
                var propertyName = element.Key;
                var propertySetterFn = (Action<T, string>)(async (dto, columnValue) =>
                {
                    var fitnesseValue = await element.Value.Converter.Convert(columnValue);
                    element.Value.SetPropertyValueFn(dto, fitnesseValue);
                });

                this.propertySetterFunctions.Add(propertyName, propertySetterFn);
            }
        }

        private void AggregateCustomColumnMappingsIfAny(Dictionary<string, ColumnMap<T>> columnNameWithMapping)
        {
            this.SetupCustomColumnMappings();

            foreach (var columnMapping in this.columnMappings)
            {
                var columnName = columnMapping.ColumnName.ToPropertyCase().ToUpper();
                if (!columnNameWithMapping.ContainsKey(columnName))
                {
                    columnNameWithMapping.Add(columnName, columnMapping);
                }
            }
        }

        private ColumnMap<T> CreateColumnMapping<T>(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            var methodInfo = typeof(ConverterManager).GetMethod("GetConverter").MakeGenericMethod(propertyType);
            
            var converter = (IConverter)methodInfo.Invoke(null, null);

            var setPropertyValueFn = (Action<T, object>)((dto, value) => property.SetValue(dto, value));

            return new ColumnMap<T>(property.Name, converter, setPropertyValueFn);
        }
    }
}
