using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(IDataMinerConnection dataMinerConnection) : ControllerBase
    {

        [HttpGet("GetDeviceByName")]
        public async Task<ActionResult<Sensor?>> GetDeviceByName([FromQuery] string deviceName, CancellationToken cancellationToken)
        {
            var result = await dataMinerConnection.GetDeviceByName(deviceName,cancellationToken);
            if(result==null) return BadRequest("No device found");

            return result;
        }


        [HttpGet("GetAllDevices")]
        public async Task<ActionResult<List<Sensor>?>> GetAllDevices(CancellationToken cancellationToken)
        {
            var result = await dataMinerConnection.GetAllDevices(cancellationToken);

            if (result == null) return BadRequest("No devices found");
            if (result.Count<=0) return BadRequest("No devices found");


            return result;
        }

        [HttpGet("GetDevicesByRoomName")]
        public async Task<ActionResult<List<Sensor>?>> GetDevicesByRoomName([FromQuery] string roomName, CancellationToken cancellationToken)
        {
            var result = await dataMinerConnection.GetDeviceByCategoryName(roomName,cancellationToken);
            if (result == null) return BadRequest($"No devices registered in {roomName}");
            if (result.Count <= 0) return BadRequest($"No devices registered in {roomName}");

            return result;
        }
    }
}
