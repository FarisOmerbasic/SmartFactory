using System;

namespace SmartFactoryBackend.Sensors
{
    public class TrackingSensor : Sensor
    {
        public bool IsItemScanned { get; private set; }

        public TrackingSensor(string sensorId, AlarmTemplate alarmTemplate)
            : base(sensorId, "Tracking Sensor", alarmTemplate)
        {
            IsItemScanned = false;
        }

        public void ScanItem()
        {
            IsItemScanned = new Random().Next(0, 2) == 1;

            Console.WriteLine($"{Name} ({Id}) - Item Scanned: {IsItemScanned}");

            string alertLevel = GetAlertLevel();
            Console.WriteLine($"ALERT LEVEL: {alertLevel}");
        }

        public override string GetAlertLevel()
        {
            return base.GetAlertLevel(IsItemScanned ? 1 : 0);
        }
    }
}
