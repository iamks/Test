using System.Collections;
using System.Collections.Concurrent;

namespace Test.Fixtures.Utils.DecisionTable
{
    public static class DecisionTableEntriesManager
    {
        private static readonly ConcurrentDictionary<string, IDictionary> EntryDictionaries
            = new ConcurrentDictionary<string, IDictionary>();

        public static void AddEntry<T>(string entryId, T entry)
        {
            var currentEntryDictionary = GetDictionaryForCurrentEntry<T>();
            if (currentEntryDictionary.ContainsKey(entryId))
            {
                throw new Exception("A record with the same entryId(FitnesseId) already exists");
            }

            currentEntryDictionary.TryAdd(entryId, entry);
        }

        public static T GetEntry<T>(string entryId)
        {
            if (GetDictionaryForCurrentEntry<T>().TryGetValue(entryId, out T entry))
            {
                return entry;
            }

            throw new Exception($"There's no entry added with key {typeof(T).FullName} with fitnesse Id {entryId}");
        }

        public static IEnumerable<T> GetAllEntries<T>()
        {
            return GetDictionaryForCurrentEntry<T>().Values.Select(entry => entry);
        }

        internal static void ClearData()
        {
            foreach (var entry in EntryDictionaries)
            {
                entry.Value.Clear();
            }
        }


        private static IDictionary<string, T> GetDictionaryForCurrentEntry<T>()
        {
            var fullName = typeof(T).FullName;

            IDictionary dictionary;
            if (!EntryDictionaries.TryGetValue(fullName, out dictionary))
            {
                dictionary = new Dictionary<string, T>();
                EntryDictionaries.TryAdd(fullName, dictionary);
            }

            return dictionary as IDictionary<string, T>;
        }
    }
}
