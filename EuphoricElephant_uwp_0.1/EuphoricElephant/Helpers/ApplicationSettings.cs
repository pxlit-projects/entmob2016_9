using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Helpers
{
    public static class ApplicationSettings
    {
        private static Dictionary<string, object> _settingsCache = new Dictionary<string, object>();

        public static void AddItem(string itemKey, object itemValue)
        {
            _settingsCache.Add(itemKey, itemValue);
        }

        public static object GetItem(string itemKey)
        {
            return _settingsCache[itemKey];
        }

        public static bool Contains(string itemKey)
        {
            return _settingsCache.ContainsKey(itemKey);
        }
        
        public static void Remove(string itemKey)
        {
            _settingsCache.Remove(itemKey);
        }

        public static void Edit(string itemKey, object itemValue)
        {
            Remove(itemKey);
            AddItem(itemKey, itemValue);
        }
    }
}
