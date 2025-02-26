using System;

namespace SmartFactoryBackend.Sensors
{
    public class WaterFlowSensor : Sensor
    {
        public double FlowRate { get; private set; }

        public WaterFlowSensor(string sensorId, AlarmTemplate alarmTemplate)
            : base(sensorId, "Water Flow Sensor", alarmTemplate)
        {
            FlowRate = new Random().NextDouble() * 50;
        }

        public void ReadData()
        {
            FlowRate = new Random().NextDouble() * 50;
            Console.WriteLine($"{Name} ({Id}) - Water Flow Rate: {FlowRate:F2} L/min");

            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(FlowRate);
        }
    }
}
