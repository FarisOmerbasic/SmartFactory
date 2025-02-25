namespace SmartFactoryBackend.Sensors
{
    public class EnergyMeterSensor : Sensor
    {
        public double EnergyUsage { get; set; }

        
        public EnergyMeterSensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Energy Meter", alarmTemplate)
        {
            EnergyUsage = new Random().Next(100, 1000);
        }

      
        public double GetSensorValue()
        {
            return EnergyUsage;
        }


        public override string GetAlertLevel()
        {
            return base.GetAlertLevel();  
    }
}
