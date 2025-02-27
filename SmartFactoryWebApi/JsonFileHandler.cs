using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartFactoryWebApi.Dtos;

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

        public int UpdateJson(UpdateThresholdDto request)
        {
            JObject jsonObj = ReadJson();

            JArray thresholds = (JArray)jsonObj["Thresholds"];

            if (thresholds == null)
            {
                return 1;
            }

            var thresholdObject = thresholds.FirstOrDefault(obj => (string)obj["id"] == request.DeviceId.ToString());


            if (thresholdObject != null)
            {
                thresholdObject[nameof(request.criticalLowThreshold)]=request.criticalLowThreshold;
                thresholdObject[nameof(request.criticalHighThreshold)] = request.criticalHighThreshold;
                thresholdObject[nameof(request.WarningLowThreshold)] = request.WarningLowThreshold;
                thresholdObject[nameof(request.WarningHighThreshold)] = request.WarningHighThreshold;
                thresholdObject[nameof(request.normalLowThreshold)] = request.normalHighThreshold;
                thresholdObject[nameof(request.normalHighThreshold)] = request.normalHighThreshold;

                File.WriteAllText(_filePath, jsonObj.ToString(Formatting.Indented));

                return 0;
            }
            else
                return 1;

        }

    }
}
