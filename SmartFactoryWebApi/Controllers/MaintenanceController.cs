using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Dtos;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IDataMinerConnection _dataMinerConnection;

        public MaintenanceController(IDataMinerConnection dataMinerConnection)
            => _dataMinerConnection = dataMinerConnection;

        [HttpGet("DashboardData")]
        public async Task<ActionResult<MaintenanceDataDto>> GetDashboardData(CancellationToken cancellationToken)
        {
            // Fetch data from DataMiner
            var historySensors = await _dataMinerConnection.GetDeviceByCategoryName("MaintenanceHistory", cancellationToken);
            var scheduledSensors = await _dataMinerConnection.GetDeviceByCategoryName("ScheduledMaintenance", cancellationToken);

            if (historySensors == null || scheduledSensors == null)
                return BadRequest("Failed to fetch maintenance data.");

            // Map Sensor data to MaintenanceTask MODELS (not DTOs)
            var historyTasks = historySensors.Select(s => new MaintenanceTask
            {
                Machine = s.Name,
                Task = s.Group1,
                Cost = s.NumericValue,
                Downtime = s.Group2,
                Status = "Completed"
            }).ToList();

            // Calculate metrics using MODELS
            var totalCost = MaintenanceService.CalculateTotalCost(historyTasks);
            var totalDowntime = MaintenanceService.CalculateTotalDowntime(historyTasks);
            var suggestions = MaintenanceService.GenerateOptimizationSuggestions(historyTasks);

            // Map to DTOs for the response
            var historyDtos = historyTasks.Select(t => new MaintenanceTaskDto
            {
                Machine = t.Machine,
                Task = t.Task,
                Cost = t.Cost,
                Downtime = t.Downtime,
                Status = t.Status
            }).ToList();

            var scheduledDtos = scheduledSensors.Select(s => new ScheduledMaintenanceDto
            {
                Machine = s.Name,
                Task = s.Group1,
                ScheduledTime = s.Group2,
                ExpectedDuration = s.Group3
            }).ToList();

            return Ok(new MaintenanceDataDto
            {
                History = historyDtos,
                Scheduled = scheduledDtos,
                TotalCost = totalCost,
                TotalDowntime = $"{totalDowntime}h",
                OptimizationSuggestions = suggestions
            });
        }
    }
}