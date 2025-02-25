using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public class WaterFlowSensor:Sensor
    {
        public double FlowRate { get;  set; } 

        public WaterFlowSensor(string sensorId) : base(sensorId, "Water Flow Sensor") { }

        public  void ReadData()
        {
            FlowRate = new Random().NextDouble() * 50;
            Console.WriteLine($"{Name} - Water Flow Rate: {FlowRate:F2} L/min");
        }
    }
}
