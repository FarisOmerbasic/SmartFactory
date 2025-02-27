using SmartFactoryWebApi.Models;

namespace SmartFactoryWebApi.InMemoryRepositories
{
    public static class MachineRepository
    {
        public static List<Machine> Machines { get; private set; } = new List<Machine>()
        {
             new Machine
             {
                 MachineId = "M001",
                 MachineName = "CNC Machine 1",
                 RoomName = "Manufacturing Hall A",
                 CurrentTemperature = 75,
                 UpTime = 70,
                 IsOperational = true,
                 IsActive = true
             },
             new Machine
             {
                 MachineId = "M002",
                 MachineName = "CNC Machine 2",
                 RoomName = "Manufacturing Hall B",
                 CurrentTemperature = 85,
                 UpTime = 120.5,
                 IsOperational = true,
                 IsActive = false
             },
             new Machine
             {
                 MachineId = "M003",
                 MachineName = "CNC Machine 3",
                 RoomName = "Plastics Department",
                 CurrentTemperature = 220,
                 UpTime = 200.0,
                 IsOperational = true,
                 IsActive = true
             },
             new Machine
             {
                 MachineId = "M004",
                 MachineName = "CNC Machine 4",
                 RoomName = "Precision Cutting Zone",
                 CurrentTemperature = 60,
                 UpTime = 45.0,
                 IsOperational = false,
                 IsActive = true
             },
             new Machine
             {
                 MachineId = "M005",
                  MachineName = "CNC Machine 5",
                 RoomName = "Prototyping Lab",
                 CurrentTemperature = 200,
                 UpTime = 10.5,
                 IsOperational = true,
                 IsActive = true
             }
        };
    }
}
