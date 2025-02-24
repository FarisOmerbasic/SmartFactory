using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    class HumiditySensor : Sensor
    {
        public double CurrentHumidity { get; set; }
        public HumiditySensor(string sensorId) : base(sensorId, "Humidity")
        {

            CurrentHumidity = new Random().Next(30, 61);
        }
        public  double GetSensorValue()
        {
            return CurrentHumidity;
        }
    }
}
