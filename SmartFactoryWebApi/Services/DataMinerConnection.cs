using Microsoft.AspNetCore.Mvc;

namespace SmartFactoryWebApi.Services
{
    public class DataMinerConnection : IDataMinerConnection
    {
        private readonly HttpClient _client;

        public DataMinerConnection(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DataMinerApi");
        }



        public async Task<string> GetAllCategories()
        {

            var response = await _client.GetAsync("getAllCategories");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAllDevices()
        {
            
            var response= await _client.GetAsync("getAllDevices");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDeviceByName(string deviceName)
        {

            var response = await _client.GetAsync($"getDevice?name={deviceName}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDeviceByCategoryName(string categoryName)
        {

            var response = await _client.GetAsync($"getCategory?name={categoryName}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
