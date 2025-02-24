using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    class AirQualitySensor : Sensor
    {
        public double AirQualityIndex { get; set; }
        public AirQualitySensor(string sensorId) : base(sensorId, "Air Quality")
        {

            AirQualityIndex = new Random().Next(0, 501);
        }
        public double GetSensorValue()
        {
            return AirQualityIndex;
        }
    }
}
