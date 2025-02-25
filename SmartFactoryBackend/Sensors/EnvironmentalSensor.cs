using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public class EnvironmentalSensor : Sensor
    {
        public double Temperature { get; set; }

        public EnvironmentalSensor(string sensorId) : base(sensorId, "Environmental Sensor")
        {
            Temperature = 20.0; 
        }
    }
}
