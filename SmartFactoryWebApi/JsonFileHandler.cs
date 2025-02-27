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

        public int UpdateJson(string id,string keyUpper, double newValueUpper, string keyLower, double newValueLower)
        {
            JObject jsonObj = ReadJson();

            JArray thresholds = (JArray)jsonObj["Thresholds"];

            if (thresholds == null)
            {
                return 1;
            }

            var thresholdObject = thresholds.FirstOrDefault(obj => (string)obj["id"] == id);


            if (thresholdObject != null)
            {
                thresholdObject[keyUpper]=newValueUpper;
                thresholdObject[keyLower]=newValueLower;
                File.WriteAllText(_filePath, jsonObj.ToString(Formatting.Indented));

                return 0;
            }
            else
                return 1;

        }

    }
}
