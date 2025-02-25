using System;

namespace SmartFactoryBackend.Sensors
{
    public class EnvironmentalSensor : Sensor
    {
        public double Temperature { get; set; } 
        public double Humidity { get; set; }
        public double AirPressure { get; set; }

        
        public double TemperatureThreshold { get; set; } = 30.0;
        public double HumidityThreshold { get; set; } = 70.0; 
        public double AirPressureThreshold { get; set; } = 1013.25;

        public EnvironmentalSensor(string sensorId) : base(sensorId, "Environmental Sensor")
        {
            Temperature = 20.0; 
            Humidity = 50.0; 
            AirPressure = 1013.25; 
        }

        public void UpdateReadings(double temperature, double humidity, double airPressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            AirPressure = airPressure;
        }

        public void CheckEnvironmentalConditions()
        {
            try
            {
                bool isTemperatureSafe = Temperature <= TemperatureThreshold;
                bool isHumiditySafe = Humidity <= HumidityThreshold;
                bool isAirPressureSafe = Math.Abs(AirPressure - AirPressureThreshold) <= 10; 

                if (!isTemperatureSafe || !isHumiditySafe || !isAirPressureSafe)
                {
                    TriggerEnvironmentalAlert(isTemperatureSafe, isHumiditySafe, isAirPressureSafe);
                }
                else
                {
                    Console.WriteLine("Environmental conditions are within safe limits.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking environmental conditions: {ex.Message}");
            }
        }

        private void TriggerEnvironmentalAlert(bool isTemperatureSafe, bool isHumiditySafe, bool isAirPressureSafe)
        {
            Console.WriteLine("ALERT: Environmental conditions are unsafe!");

            if (!isTemperatureSafe)
            {
                Console.WriteLine($"- Temperature is too high: {Temperature} °C (Threshold: {TemperatureThreshold} °C)");
            }

            if (!isHumiditySafe)
            {
                Console.WriteLine($"- Humidity is too high: {Humidity} % (Threshold: {HumidityThreshold} %)");
            }

            if (!isAirPressureSafe)
            {
                Console.WriteLine($"- Air pressure is abnormal: {AirPressure} hPa (Threshold: {AirPressureThreshold} hPa ±10)");
            }

     
            Console.WriteLine("Action: Notify staff and adjust environmental controls.");
        }

        public void LogSensorData()
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