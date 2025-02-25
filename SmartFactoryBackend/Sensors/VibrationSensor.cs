using System;

namespace SmartFactoryBackend.Sensors
{
    public class VibrationSensor : Sensor
    {
        public double CurrentVibration { get; private set; }

        public VibrationSensor(string sensorId, AlarmTemplate alarmTemplate)
            : base(sensorId, "Vibration Sensor", alarmTemplate)
        {
            CurrentVibration = new Random().NextDouble() * (10 - 0.1) + 0.1;
        }

        public void ReadData()
        {
            CurrentVibration = new Random().NextDouble() * (10 - 0.1) + 0.1;

            Console.WriteLine($"{Name} ({Id}) - Vibration Level: {CurrentVibration:F2}");

            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(CurrentVibration);
        }
    }
}
