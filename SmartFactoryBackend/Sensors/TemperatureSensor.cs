using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    class TemperatureSensor : Sensor
    {
        public double CurrentTemperature { get; set; }
        public TemperatureSensor(string sensorId) : base(sensorId, "Temperature")
        {

            CurrentTemperature = new Random().Next(18, 27);
        }
        public  double GetSensorValue()
        {
            return CurrentTemperature;
        }
    }
}
