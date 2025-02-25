namespace SmartFactoryBackend.Sensors
{
    public class HumiditySensor : Sensor
    {
        public double CurrentHumidity { get; set; }

        
        public HumiditySensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Humidity Sensor", alarmTemplate)
        {
            CurrentHumidity = new Random().Next(30, 61);  
        }

        public void ReadData()
        {
           
            CurrentHumidity = new Random().NextDouble() * (60 - 30) + 30;  
            Console.WriteLine($"{Name} - Humidity: {CurrentHumidity:F2}%");


            Console.WriteLine($"Humidity Alert Level: {GetAlertLevel()}");
        }


        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(); 
        }
    }
}
