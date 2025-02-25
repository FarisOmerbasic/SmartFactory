namespace SmartFactoryBackend.Sensors
{
    public class MotionSensor : Sensor
    {
        public bool IsMotionDetected { get; set; }


        public MotionSensor(string id, AlarmTemplate alarmTemplate) : base(id, "Motion Sensor", alarmTemplate)
        {
     
            IsMotionDetected = new Random().Next(0, 2) == 1;
        }

  
        public bool GetSensorValue()
        {
            return IsMotionDetected;
        }


        public override string GetAlertLevel()
        {
            
            double motionValue = IsMotionDetected ? 1 : 0;  

            return base.GetAlertLevel(motionValue);
        }
    }
}
