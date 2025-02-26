using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Models;
using SmartFactoryWebApi.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly IDataMinerConnection _dataMinerConnection;

        // Inject IDataMinerConnection into the constructor
        public EnergyController(IDataMinerConnection dataMinerConnection)
        {
            _dataMinerConnection = dataMinerConnection;
        }

        // Endpoint to calculate total power consumption of devices
        [HttpGet("CalculateTotalPower")]
        public async Task<ActionResult<EnergyDto>> CalculateTotalPower(CancellationToken cancellationToken)
        {
            var devices = await _dataMinerConnection.GetAllDevices(cancellationToken);
            if (devices == null || devices.Count == 0)
                return BadRequest("No devices found to calculate power consumption.");

            var energyDevices=devices.Where(d=>d.Group2 == "Energy Meter Sensor").ToList();


            var totalPower = EnergyCalculationService.CalculateTotalPower(energyDevices);
            totalPower = totalPower / 1000;
            var cost = EnergyCalculationService.CalculateCost(totalPower);
            var suggestions = EnergyCalculationService.GenerateOptimizationSuggestions(devices);

            var effRate=new Random().Next(80, 100);

            // Returning DTO with calculated power data
            var energyDto = new EnergyDto
            {
                TotalPower = totalPower,
                TotalCost = cost,
                OptimizationSuggestions = suggestions,
                EfficiencyRate= effRate
            };

            return Ok(energyDto);
        }

        // Endpoint to calculate efficiency rate
        [HttpGet("CalculateEfficiencyRate")]
        public ActionResult<EnergyDto> CalculateEfficiencyRate([FromQuery] double usefulOutput, [FromQuery] double totalInput)
        {
            if (totalInput == 0)
                return BadRequest("Total input cannot be zero.");

            var efficiencyRate = EnergyCalculationService.CalculateEfficiencyRate(usefulOutput, totalInput);

            // Returning DTO with efficiency data
            var energyDto = new EnergyDto
            {
                EfficiencyRate = efficiencyRate
            };

            return Ok(energyDto);
        }

        // Endpoint to calculate cost of energy consumption
        [HttpGet("CalculateCost")]
        public async Task<ActionResult<EnergyDto>> CalculateCost(CancellationToken cancellationToken)
        {
            var devices = await _dataMinerConnection.GetAllDevices(cancellationToken);
            if (devices == null || devices.Count == 0)
                return BadRequest("No devices found to calculate power consumption.");

            var totalPower = EnergyCalculationService.CalculateTotalPower(devices);
            var cost = EnergyCalculationService.CalculateCost(totalPower);
            var suggestions = EnergyCalculationService.GenerateOptimizationSuggestions(devices);

            // Returning DTO with cost data
            var energyDto = new EnergyDto
            {
                TotalPower = totalPower,
                TotalCost = cost,
                OptimizationSuggestions = suggestions
            };

            return Ok(energyDto);
        }

        // Endpoint to calculate the change in power consumption
        [HttpGet("CalculateChange")]
        public ActionResult<EnergyDto> CalculateChange([FromQuery] double currentConsumption, [FromQuery] double previousConsumption)
        {
            var change = EnergyCalculationService.CalculateChange(currentConsumption, previousConsumption);

            // Returning DTO with consumption change data
            var energyDto = new EnergyDto
            {
                ConsumptionChange = change
            };

            return Ok(energyDto);
        }

        // Endpoint to generate optimization suggestions
        [HttpGet("GenerateOptimizationSuggestions")]
        public async Task<ActionResult<EnergyDto>> GenerateOptimizationSuggestions(CancellationToken cancellationToken)
        {
            var devices = await _dataMinerConnection.GetAllDevices(cancellationToken);
            if (devices == null || devices.Count == 0)
                return BadRequest("No devices found to generate optimization suggestions.");

            var suggestions = EnergyCalculationService.GenerateOptimizationSuggestions(devices);

            // Returning DTO with optimization suggestions
            var energyDto = new EnergyDto
            {
                OptimizationSuggestions = suggestions
            };

            return Ok(energyDto);
        }
    }
}
