using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IDataMinerConnection dataMinerConnection) : ControllerBase
    {
        [HttpGet("GetSensorsForRoom/{roomName}")]
        public async Task<ActionResult<List<Sensor>>> GetSensorsForRoom(string roomName,CancellationToken cancellationToken)
        {
            var sensors = await dataMinerConnection.GetDeviceByCategoryName(roomName, cancellationToken);

            if (sensors == null) return BadRequest();
            if(sensors.Count == 0) return NotFound();

           

            return sensors;
        }
    }


    public class GetSensorsForRoomResponse
    {
        public List<Sensor> Sensors { get; set; }
        public List<Machine> Machines { get; set; }
    }
}
