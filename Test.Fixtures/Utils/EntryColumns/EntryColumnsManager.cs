namespace Test.Fixtures.Utils.EntryColumns
{
    internal static class EntryColumnsManager
    {

        private static readonly IDictionary<object, IEnumerable<string>> entryWithColumnNames = new Dictionary<object, IEnumerable<string>>();

        public static void ClearData()
        {
            entryWithColumnNames.Clear();
        }

        public static void AddEntry(object entry, IEnumerable<string> columnNames)
        {
            if (entry == null)
            {
                throw new InvalidOperationException("The entry can not be null");
            }

            entryWithColumnNames.Add(entry, columnNames);
        }

        public static IEnumerable<string> GetEntryColumns(object entry)
        {
            entryWithColumnNames.TryGetValue(entry, out var columns);

            return columns;
        }
    }
}