using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Models;

namespace SmartFactoryWebApi.Services
{
    public interface IDataMinerConnection
    {
        Task<List<Sensor>?> GetAllDevices(CancellationToken cancellationToken);
        Task<List<Category>?> GetAllCategories(CancellationToken cancellationToken);
        Task<Sensor?> GetDeviceByName(string deviceName, CancellationToken cancellationToken);
        Task<List<Sensor>?> GetDeviceByCategoryName(string categoryName, CancellationToken cancellationToken);
        Task<List<SensorDataDto>?> GetDeviceTrending(int deviceId, CancellationToken cancellationToken);
    }
}
