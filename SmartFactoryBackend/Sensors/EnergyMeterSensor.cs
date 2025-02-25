namespace SmartFactoryBackend.Sensors
{
    public class EnergyMeterSensor : Sensor
    {
        public double EnergyUsage { get; set; }
        private double _upperThreshold = 5000;  
        private double _lowerThreshold = 100;   

      
        public string EnergyUsageAlert { get; private set; }

        public EnergyMeterSensor(string sensorId) : base(sensorId, "Energy Meter")
        {
            EnergyUsage = new Random().Next(100, 1000); 
            EnergyUsageAlert = CheckEnergyUsageAlert();  
        }

    
        public double GetSensorValue()
        {
            return EnergyUsage;
        }


        public void UpdateEnergyUsage()
        {
            EnergyUsage = new Random().Next(100, 5000);  
            EnergyUsageAlert = CheckEnergyUsageAlert();  
        }


        private string CheckEnergyUsageAlert()
        {
            if (EnergyUsage > _upperThreshold)
            {
                return "Alert: High Energy Consumption!";
            }
            else if (EnergyUsage < _lowerThreshold)
            {
                return "Alert: Low Energy Consumption!";
            }
            else
            {
                return "Energy Usage Normal";
            }
        }
    }
}
