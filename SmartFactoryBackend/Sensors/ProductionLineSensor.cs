using System;

namespace SmartFactoryBackend.Sensors
{
    public class ProductionLineSensor : Sensor
    {
        public double Throughput { get; private set; }

        public ProductionLineSensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Production Line Sensor", alarmTemplate)
        {
            Throughput = new Random().Next(0, 500);
        }

        public void ReadData()
        {
         
            Random rand = new Random();
            Throughput = rand.Next(0, 500); 

            Console.WriteLine($"{Name} ({Id}) - Throughput: {Throughput} units/hour");

           
            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(Throughput);
        }
    }
}
