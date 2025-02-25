using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{

    class VibrationSensor : Sensor
    {
        public double CurrentVibration { get; set; }

        public VibrationSensor(string sensorId) : base(sensorId, "Vibration")
        {
            CurrentVibration = new Random().NextDouble() * (10 - 0.1) + 0.1;
        }

        public double GetSensorValue()
        {
            return CurrentVibration;
        }
    }
}
