using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledMaintenanceController : ControllerBase
    {
        // Static list to store schedules
        private static List<ScheduledMaintenanceDto> _schedules = new List<ScheduledMaintenanceDto>
        {
            new ScheduledMaintenanceDto
            {
                Machine = "CNC Machine #3",
                Task = "Bearing Service",
                ScheduledTime = "Tomorrow, 8:00 AM",
                ExpectedDuration = "4h"
            },
            new ScheduledMaintenanceDto
            {
                Machine = "Assembly Line B",
                Task = "Motor Check",
                ScheduledTime = "Next Week, Tuesday",
                ExpectedDuration = "2h"
            }
        };

        // GET: api/ScheduledMaintenance/ScheduledMaintenance
        [HttpGet("ScheduledMaintenance")]
        public ActionResult<MaintenanceDataDto> GetScheduledMaintenance()
        {
            return Ok(new MaintenanceDataDto { Scheduled = _schedules });
        }

        // POST: api/ScheduledMaintenance/AddSchedule
        [HttpPost("AddSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduledMaintenanceDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSchedule([FromBody] ScheduledMaintenanceDto newSchedule)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new schedule to the list
            _schedules.Add(newSchedule);

            // Return the added schedule
            return Ok(newSchedule);
        }
    }
}