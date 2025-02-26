using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IDataMinerConnection _dataMinerConnection;

        // Inject IDataMinerConnection into the constructor
        public ProductionController(IDataMinerConnection dataMinerConnection)
        {
            _dataMinerConnection = dataMinerConnection;
        }

        // Endpoint to get production data for a specific line
        [HttpGet("GetProductionData/{line}")]
        public async Task<ActionResult<ProductionDto>> GetProductionData(string line, CancellationToken cancellationToken)
        {
            var devices = await _dataMinerConnection.GetAllDevices(cancellationToken);
            if (devices == null || devices.Count == 0)
                return BadRequest("No devices found to calculate production data.");

            // Filter devices for production-related sensors (if needed)
            var productionDevices = devices.Where(d => d.Group2 == "Production Sensor").ToList();

            // Calculate production metrics
            var throughput = ProductionService.CalculateThroughput(line);
            var status = ProductionService.GetStatus(line);
            var machine = ProductionService.GetMachine(line);
            var issue = ProductionService.GetIssue(line);
            var efficiency = ProductionService.CalculateEfficiency(line);
            var targetDeviation = ProductionService.CalculateTargetDeviation(line);
            var todaysProjectedOutput = ProductionService.CalculateTodaysOutput();
            var weeksProjection = ProductionService.CalculateWeeksProjection();
            var productionRate = ProductionService.CalculateProductionRate(todaysProjectedOutput, 24); // Assuming 24 hours for daily output

            // Generate optimization suggestions
            var suggestions = ProductionService.GenerateOptimizationSuggestions(new ProductionDto
            {
                Line = line,
                Efficiency = efficiency,
                TargetDeviation = targetDeviation
            });

            // Return DTO with production data
            var productionDto = new ProductionDto
            {
                Line = line,
                Throughput = throughput,
                Status = status,
                Machine = machine,
                Issue = issue,
                Efficiency = efficiency,
                TargetDeviation = targetDeviation,
                TodaysProjectedOutput = todaysProjectedOutput,
                WeeksProjection = weeksProjection,
                ProductionRate = productionRate,
                OptimizationSuggestions = suggestions
            };

            return Ok(productionDto);
        }

        // Endpoint to calculate production rate
        [HttpGet("CalculateProductionRate")]
        public ActionResult<ProductionDto> CalculateProductionRate([FromQuery] int totalUnitsProduced, [FromQuery] double totalTimeTaken)
        {
            if (totalTimeTaken <= 0)
                return BadRequest("Total time taken must be greater than zero.");

            var productionRate = ProductionService.CalculateProductionRate(totalUnitsProduced, totalTimeTaken);

            // Return DTO with production rate
            var productionDto = new ProductionDto
            {
                ProductionRate = productionRate
            };

            return Ok(productionDto);
        }

        // Endpoint to generate optimization suggestions
        [HttpGet("GenerateOptimizationSuggestions/{line}")]
        public async Task<ActionResult<ProductionDto>> GenerateOptimizationSuggestions(string line, CancellationToken cancellationToken)
        {
            var devices = await _dataMinerConnection.GetAllDevices(cancellationToken);
            if (devices == null || devices.Count == 0)
                return BadRequest("No devices found to generate optimization suggestions.");

            // Filter devices for production-related sensors (if needed)
            var productionDevices = devices.Where(d => d.Group2 == "Production Sensor").ToList();

            // Calculate production metrics
            var efficiency = ProductionService.CalculateEfficiency(line);
            var targetDeviation = ProductionService.CalculateTargetDeviation(line);

  
            var suggestions = ProductionService.GenerateOptimizationSuggestions(new ProductionDto
            {
                Line = line,
                Efficiency = efficiency,
                TargetDeviation = targetDeviation
            });

            var productionDto = new ProductionDto
            {
                OptimizationSuggestions = suggestions
            };

            return Ok(productionDto);
        }
    }
}