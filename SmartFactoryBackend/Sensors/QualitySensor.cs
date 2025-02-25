using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    class QualitySensor : Sensor
    {
        public double CurrentAccuracy { get; set; }

        public QualitySensor(string sensorId) : base(sensorId, "QualitySensor")
        {
            CurrentAccuracy = new Random().NextDouble() * (0.5 - 0.01) + 0.01;
        }

        public double GetSensorValue()
        {
            return CurrentAccuracy;
        }
    }
}
