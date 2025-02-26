namespace SmartFactoryBackend.Sensors
{
    public class OccupancySensor : Sensor
    {
        public bool IsOccupied { get; set; }

        
        public OccupancySensor(string sensorId, AlarmTemplate alarmTemplate) : base(sensorId, "Occupancy Sensor", alarmTemplate)
        {
            IsOccupied = new Random().Next(0, 2) == 1; 
        }

        public double GetSensorValue()
        {
            return IsOccupied ? 1 : 0;
        }

        public override string GetAlertLevel()
        {
            double occupancyValue = IsOccupied ? 1 : 0; 

            return base.GetAlertLevel(occupancyValue);
        }
    }
}
