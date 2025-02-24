using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public class Sensor
    {
        public string SensorId { get; set; }
        public string SensorType { get; set; }
        public bool IsActive { get; set; }

        public  double GetSensorValue(Sensor SensorID)
        {
            return 0;
        }

        public void ToggleSensor(bool status)
        {
            IsActive = status;
        }

        public Sensor(string sensorId, string sensorType)
        {
            SensorId = sensorId;
            SensorType = sensorType;
            IsActive = true;
        }
    }
}
