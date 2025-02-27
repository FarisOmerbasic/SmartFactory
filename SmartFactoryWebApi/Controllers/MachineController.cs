using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.InMemoryRepositories;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        [HttpGet("machineOverview")]
        public ActionResult<MachineOverviewDto> MachineOverview()
        {
            var machineOverview = new MachineOverviewDto
            {
                RunningMachines = 30,
                WarningMachinesThreshold = 3,
                CriticalMachinesThreshold = 1,
                IdleMachines = 3,
                Machines = MachineRepository.Machines
            };

            return machineOverview;
        }
    }
}
