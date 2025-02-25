using System;

namespace SmartFactoryBackend.Sensors
{
    public class GasLeakSensor : Sensor
    {
        public double GasConcentration { get; set; }

        private double _upperThreshold = 5.0; 
        private double _lowerThreshold = 0.1;  


       
        public string GasLeakAlert { get; private set; }

        public GasLeakSensor(string sensorId) : base(sensorId, "Gas Leak Sensor")
        {
            GasConcentration = 0;
            GasLeakAlert = "Normal"; 
        }

        
        public void ReadData()
        {
            GasConcentration = new Random().NextDouble() * 10;  
            Console.WriteLine($"{Name} - Gas Concentration: {GasConcentration:F2} ppm");

            GasLeakAlert = CheckGasLeakAlert();
            Console.WriteLine($"Alert: {GasLeakAlert}");
        }


        private string CheckGasLeakAlert()
        {
            if (GasConcentration > _upperThreshold)
            {
                return "Alert: High Gas Concentration!";
            }
            else if (GasConcentration < _lowerThreshold)
            {
                return "Alert: Gas Concentration Too Low!";
            }
            else
            {
                return "Gas Concentration Normal";
            }
        }
    }
}
