using Microsoft.AspNetCore.Mvc;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Services;
using System.Collections.Generic;

namespace SmartFactoryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionController : ControllerBase
    {
        private readonly ProductionService _productionService;

        public ProductionController()
        {
            _productionService = new ProductionService();
        }

        [HttpGet("GetProductionData/{line}")]
        public ActionResult<ProductionDto> GetProductionData(string line)
        {
            var throughput = _productionService.CalculateThroughput(line);
            var status = _productionService.GetStatus(line);
            var machine = _productionService.GetMachine(line);
            var issue = _productionService.GetIssue(line);
            var efficiency = _productionService.CalculateEfficiency(line);
            var targetDeviation = _productionService.CalculateTargetDeviation(line);
            var todaysProjectedOutput = _productionService.CalculateTodaysOutput(line);
            var weeksProjection = _productionService.CalculateWeeksProjection(line);
            var productionRate = _productionService.CalculateProductionRate(todaysProjectedOutput, 8);

            var suggestions = _productionService.GenerateOptimizationSuggestions(new ProductionDto
            {
                Line = line,
                Efficiency = efficiency,
                TargetDeviation = targetDeviation
            });

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

       
}
}
