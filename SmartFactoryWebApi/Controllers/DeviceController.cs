using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(IDataMinerConnection dataMinerConnection) : ControllerBase
    {

        [HttpGet("GetDeviceByName")]
        public async Task<ActionResult<string?>> GetDeviceByName([FromQuery] string deviceName)
        {
            var result = await dataMinerConnection.GetDeviceByName(deviceName);

            return result;
        }


        [HttpGet("GetAllDevices")]
        public async Task<ActionResult<string?>> GetAllCategories()
        {
            var result = await dataMinerConnection.GetAllDevices();


            return result;
        }

        [HttpGet("GetDevicesByRoomName")]
        public async Task<ActionResult<string?>> GetDevicesByRoomName([FromQuery] string roomName)
        {
            var result = await dataMinerConnection.GetDeviceByCategoryName(roomName);

            return result;
        }
    }
}
