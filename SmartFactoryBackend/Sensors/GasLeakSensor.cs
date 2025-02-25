namespace SmartFactoryBackend.Sensors
{
    public class GasLeakSensor : Sensor
    {
        public double GasConcentration { get; set; }


        public GasLeakSensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Gas Leak Sensor", alarmTemplate) { }

   
        public void ReadData()
        {
            GasConcentration = new Random().NextDouble() * 10;
            Console.WriteLine($"{Name} - Gas Concentration: {GasConcentration:F2} ppm");

          
            Console.WriteLine($"Gas Leak Alert Level: {GetAlertLevel()}");
        }

        
        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(); 
        }
    }
}
