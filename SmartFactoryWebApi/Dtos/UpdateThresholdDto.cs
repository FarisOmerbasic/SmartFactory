namespace SmartFactoryWebApi.Dtos
{
    public class UpdateThresholdDto
    {
        public int DeviceId { get; set; }
        public ThresholdTypes Type { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
    }
}
