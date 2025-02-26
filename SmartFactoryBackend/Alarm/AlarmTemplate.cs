namespace SmartFactoryBackend.Sensors
{
    public class AlarmTemplate
    {
        public double CriticalLowThreshold { get; set; }
        public double NormalThresholdLow { get; set; }
        public double NormalThresholdHigh { get; set; }
        public double CriticalHighThreshold { get; set; }

        public AlarmTemplate(double criticalLow, double normalLow, double normalHigh, double criticalHigh)
        {
            CriticalLowThreshold = criticalLow;
            NormalThresholdLow = normalLow;
            NormalThresholdHigh = normalHigh;
            CriticalHighThreshold = criticalHigh;
        }


        public string GetAlertLevel(double currentValue)
        {
            if (currentValue < CriticalLowThreshold)
                return "Critical Low";
            if (currentValue >= CriticalLowThreshold && currentValue < NormalThresholdLow)
                return "Normal";
            if (currentValue >= NormalThresholdLow && currentValue <= NormalThresholdHigh)
                return "Warning";
            if (currentValue > NormalThresholdHigh && currentValue <= CriticalHighThreshold)
                return "Critical High";
            return "Normal";
        }
    }
}
