using System;

namespace SmartFactoryBackend.Sensors
{
    public class TemperatureSensor : Sensor
    {
        public double CurrentTemperature { get; private set; }

        public TemperatureSensor(string sensorId, AlarmTemplate alarmTemplate)
            : base(sensorId, "Temperature Sensor", alarmTemplate)
        {
            CurrentTemperature = new Random().Next(18, 27);
        }

        public void ReadData()
        {
   
            Random rand = new Random();
            CurrentTemperature = rand.Next(10, 100);

            Console.WriteLine($"{Name} ({Id}) - Current Temperature: {CurrentTemperature}°C");

            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(CurrentTemperature);
        }
    }
}
