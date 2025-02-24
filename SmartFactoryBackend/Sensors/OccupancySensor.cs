using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    class OccupancySensor : Sensor
    {
        public bool IsOccupied { get; set; }
        public OccupancySensor(string sensorId) : base(sensorId, "Occupancy")
        {

            IsOccupied = new Random().Next(0, 2) == 1;
        }
        public  double GetSensorValue()
        {
            return IsOccupied ? 1 : 0;
        }
    }
}
