using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public class TrackingSensor : Sensor
    {
        public bool IsItemScanned { get; set; }

        public TrackingSensor(string sensorId) : base(sensorId, "Tracking Sensor")
        {
            IsItemScanned = false;
        }
    }
}