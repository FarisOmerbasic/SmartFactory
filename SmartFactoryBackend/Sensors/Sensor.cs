namespace SmartFactoryBackend.Sensors
{
    public class Sensor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double NumericValue { get; set; }
        public string StringValue { get; set; }
        public string Unit { get; set; }
        public int SimulationType { get; set; }
        public double GrowthRatio { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Group3 { get; set; }
        public bool IsActive { get; set; }
        public int UpdateInterval { get; set; }
        public string SensorType { get; set; }
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
        public AlarmTemplate AlarmTemplate { get; set; }  

        public Sensor() { }

        public Sensor(string sensorId, string sensorType, AlarmTemplate alarmTemplate)
        {
            Id = sensorId;
            Name = sensorType;
            AlarmTemplate = alarmTemplate; 
            IsActive = true;
        }

    
        public virtual string GetAlertLevel()
        {
            if (NumericValue < AlarmTemplate.CriticalLow)
                return "Critical Low";
            else if (NumericValue >= AlarmTemplate.CriticalLow && NumericValue < AlarmTemplate.Normal)
                return "Normal";
            else if (NumericValue >= AlarmTemplate.Normal && NumericValue < AlarmTemplate.Warning)
                return "Warning";
            else if (NumericValue >= AlarmTemplate.Warning)
                return "Critical High";
            return "Normal";  
        }

        public void ToggleSensor(bool status)
        {
            IsActive = status;
        }
    }
}
