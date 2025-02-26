using System;

namespace SmartFactoryBackend.Sensors
{
    public class WeightSensor : Sensor
    {
        public double Weight { get; set; }
        public AlarmTemplate Alarm { get; set; }  

        public WeightSensor(string id, AlarmTemplate alarm) : base(id, "Weight")
        {
            Weight = new Random().NextDouble() * 100;
            Alarm = alarm;
        }

        public double GetSensorValue()
        {
            return Weight;
        }

        public void ReadData()
        {
            Weight = new Random().NextDouble() * 100;  
            Console.WriteLine($"{Name} ({Id}) - Weight: {Weight:F2} kg");

            if (Alarm?.CheckAlarm(Weight) == true)
            {
                Console.WriteLine($"ALERT: {Name} ({Id}) weight out of bounds! Current: {Weight:F2} kg");
            }
        }
    }
}
