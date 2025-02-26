﻿namespace SmartFactoryBackend.Sensors
{
    public class EnvironmentalSensor : Sensor
    {
        public double Temperature { get; set; }

 
        public EnvironmentalSensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Environmental Sensor", alarmTemplate)
        {
            Temperature = 20.0;  
        }


        public void ReadData()
        {
           
            Temperature = new Random().NextDouble() * (35 - 15) + 15;  
            Console.WriteLine($"{Name} - Temperature: {Temperature:F2}°C");

    
            Console.WriteLine($"Temperature Alert Level: {GetAlertLevel()}");
        }


        public override string GetAlertLevel()
        {
            Console.WriteLine($"Logging Environmental Sensor Data:");
            Console.WriteLine($"- Sensor ID: {SensorId}");
            Console.WriteLine($"- Temperature: {Temperature} °C");
            Console.WriteLine($"- Humidity: {Humidity} %");
            Console.WriteLine($"- Air Pressure: {AirPressure} hPa");
            Console.WriteLine($"- Timestamp: {DateTime.Now}");
        }
    }
}
