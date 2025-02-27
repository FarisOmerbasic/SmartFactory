namespace SmartFactoryWebApi.Dtos
{
    public class UpdateThresholdDto
    {
        public int DeviceId { get; set; }

        public double normalLowThreshold { get; set; }
        public double normalHighThreshold { get; set; }
        public double WarningLowThreshold { get; set; }
        public double WarningHighThreshold { get; set; }
        public double criticalLowThreshold { get; set; }
        public double criticalHighThreshold { get; set; }
    }
}
