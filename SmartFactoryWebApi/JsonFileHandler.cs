using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartFactoryWebApi
{
    public class JsonFileHandler
    {

        private readonly string _filePath;

        public JsonFileHandler(string filePath)
        {
            _filePath = filePath;
        }


        public static string GetJsonFilePath(string fileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, fileName);
        }

        public JObject ReadJson()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("JSON file not found.", _filePath);
            }

            string json = File.ReadAllText(_filePath);
            return JObject.Parse(json);
        }

        public void UpdateJson(string key, string newValue)
        {
            JObject jsonObj = ReadJson();

            if (jsonObj.ContainsKey(key))
            {
                jsonObj[key] = newValue;

                File.WriteAllText(_filePath, jsonObj.ToString(Formatting.Indented));
                Console.WriteLine($"Updated '{key}' in JSON file.");
            }
            else
            {
                Console.WriteLine($"Key '{key}' not found in JSON.");
            }
        }

    }
}
