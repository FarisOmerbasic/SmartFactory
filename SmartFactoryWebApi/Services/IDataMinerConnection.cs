using Microsoft.AspNetCore.Mvc;

namespace SmartFactoryWebApi.Services
{
    public interface IDataMinerConnection
    {
        Task<string> GetAllDevices();
        Task<string> GetAllCategories();
        Task<string> GetDeviceByName(string deviceName);
        Task<string> GetDeviceByCategoryName(string categoryName);
    }
}
