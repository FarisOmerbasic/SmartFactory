using System;

namespace SmartFactoryBackend.Sensors
{
    public class PressureSensor : Sensor
    {
        public double Pressure { get; private set; } 

        public PressureSensor(string id, AlarmTemplate alarmTemplate) : base(id, "Pressure Sensor", alarmTemplate) { }

        public void ReadData()
        {
            
            Random rand = new Random();
            Pressure = rand.NextDouble() * 10 + 5; 

            Console.WriteLine($"{Name} ({Id}) - Pressure: {Pressure:F2} Bar");


            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(Pressure);
        }
    }
}
