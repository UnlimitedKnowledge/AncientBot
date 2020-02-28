using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace AncientBot
{
    class DataStore
    {
        private static Dictionary<string, string> pairs = new Dictionary<string, string>();

        public static void AddPairToStorage(string key, string value)
        {
            pairs.Add(key, value);
            SaveData();
        }

        public static int GetPairsCount()
        {
            return pairs.Count;
        }

        static DataStore()
        {
            // Load data
            if (!ValidateStorageFile("DateStore.json")) return;
            string json = File.ReadAllText("DataStore.json");
            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static void SaveData()
        {
            //Save data
            string json = JsonConvert.SerializeObject(pairs, Formatting.Indented);
            File.WriteAllText("DataStore.json", json);
        }

        private static bool ValidateStorageFile(string file)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "");
                SaveData();
                return false;
            }
            return true;
        }
    }
}
