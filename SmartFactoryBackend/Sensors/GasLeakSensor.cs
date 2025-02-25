using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartFactoryBackend.Sensors
{
    public class GasLeakSensor:Sensor
    {
        public double GasConcentration { get;  set; } 

        public GasLeakSensor(string sensorId) : base( sensorId, "Gas Leak Sensor") { }

        public void ReadData()
        {
            GasConcentration = new Random().NextDouble() * 10;
            Console.WriteLine($"{Name} - Gas Concentration: {GasConcentration:F2} ppm");
        }
    }
}
