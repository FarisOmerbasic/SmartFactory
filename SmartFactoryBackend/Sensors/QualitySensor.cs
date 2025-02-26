using System;

namespace SmartFactoryBackend.Sensors
{
    public class QualitySensor : Sensor
    {
        public double CurrentAccuracy { get; private set; }

        public QualitySensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Quality Sensor", alarmTemplate)
        {
            CurrentAccuracy = new Random().NextDouble() * (0.5 - 0.01) + 0.01;
        }

        public void ReadData()
        {
         
            Random rand = new Random();
            CurrentAccuracy = rand.NextDouble() * (0.5 - 0.01) + 0.01; 

            Console.WriteLine($"{Name} ({Id}) - Current Accuracy: {CurrentAccuracy:P2}");

           
            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(CurrentAccuracy);
        }
    }
}
