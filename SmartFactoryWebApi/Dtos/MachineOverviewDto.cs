using SmartFactoryWebApi.Models;
using System.Reflection.PortableExecutable;

namespace SmartFactoryWebApi.Dtos
{
    public class MachineOverviewDto
    {
        public int RunningMachines { get; set; }
        public int WarningMachinesThreshold { get; set; }
        public int CriticalMachinesThreshold { get; set; }
        public int IdleMachines { get; set; }
        public List<Models.Machine> Machines { get; set; }

    }
}
