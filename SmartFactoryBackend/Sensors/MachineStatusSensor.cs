using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public enum MachineState { Running, Idle, Malfunctioning }
    public class MachineStatusSensor : Sensor
    {
      
        public MachineState CurrentState { get; private set; }

        public MachineStatusSensor(string id) : base(id, "Machine Status Sensor") { }

        public void ReadData()
        {
            Random rand = new Random();
            int state = rand.Next(0, 3);
            CurrentState = (MachineState)state;

            Console.WriteLine($"{Name} ({Id}) - Current State: {CurrentState}");
        }
    }
}
