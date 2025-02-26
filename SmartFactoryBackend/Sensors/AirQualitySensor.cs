using System;

namespace SmartFactoryBackend.Sensors
{
    class AirQualitySensor : Sensor
    {
        public double AirQualityIndex { get; set; }

        public AirQualitySensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Air Quality", alarmTemplate)
        {
          
            AirQualityIndex = new Random().Next(0, 501);
        }

        public double GetSensorValue()
        {
            return AirQualityIndex;
        }


        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(); 
        }
    }
}
