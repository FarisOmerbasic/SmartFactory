using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Models;
using System.Text.Json;

namespace SmartFactoryWebApi.Services
{
    public class DataMinerConnection : IDataMinerConnection
    {
        private readonly HttpClient _client;

        public DataMinerConnection(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DataMinerApi");
        }



        public async Task<List<Category>?> GetAllCategories(CancellationToken cancellationToken)
        {

            var response = await _client.GetAsync("getAllCategories",cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new List<Category>();

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = JsonSerializer.Deserialize<List<Category>>(responseString, options);
            return result;
        }

        public async Task<List<Sensor>?> GetAllDevices(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync("getAllDevices",cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new List<Sensor>();

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = JsonSerializer.Deserialize<List<Sensor>>(responseString, options);
            return result;

        }

        public async Task<Sensor?> GetDeviceByName(string deviceName, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"getDevice?name={deviceName}",cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var desrializedJson = JsonSerializer.Deserialize<List<Sensor>>(responseString, options);

            if (desrializedJson == null || desrializedJson.Count<=0)
            {
                return null;
            }

            var result = desrializedJson[0];

            return result;

        }

        public async Task<List<Sensor>?> GetDeviceByCategoryName(string categoryName, CancellationToken cancellationToken)
        {

            var response = await _client.GetAsync($"getCategory?name={categoryName}",cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};

            var result = JsonSerializer.Deserialize<List<Sensor>>(responseString, options);

            if (result == null || result.Count <= 0)
                return null;

            return result;
        }

        public async Task<List<SensorDataDto>?> GetDeviceTrending(int deviceId, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"getTrendingInfo?id={deviceId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new List<SensorDataDto>(); 

            var stream = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var trendingData = JsonSerializer.Deserialize<Trending>(stream, options);


            var sensorList = trendingData.Records.Values
                  .SelectMany(records => records)
                  .Select(record => new SensorDataDto
                  {
                      Time = record.Time.ToString("HH:mm:ss"),
                      Value = double.Parse(record.Value)
                  })
                  .ToList();

            return sensorList;
        }


        public async Task<List<SensorRecordAverage>?> GetDeviceTrendingAverage(int deviceId, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"getTrendingInfo?id={deviceId}&&type=average", cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new List<SensorRecordAverage>();

            var stream = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var trendingData = JsonSerializer.Deserialize<TrendingAverage>(stream, options);


            var records=trendingData.Records.Values
                  .SelectMany(records => records)
                  .Select(record => new SensorRecordAverage
                  {
                      AverageValue=record.AverageValue,
                      MinimumValue=record.MinimumValue,
                      MaximumValue=record.MaximumValue,
                      Status=record.Status,
                      Time=record.Time
                  })
                  .ToList();

            return records;
        }
    }
}
