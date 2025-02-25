using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public class PressureSensor : Sensor
    {
        public double Pressure { get; private set; } // Measured in Bar

        public PressureSensor(string id) : base(id, "Pressure Sensor") { }

        public void ReadData()
        {
            // Simulate pressure reading
            Random rand = new Random();
            Pressure = rand.NextDouble() * 10 + 5; // 5-15 Bar

            Console.WriteLine($"{Name} ({Id}) - Pressure: {Pressure:F2} Bar");

            if (Pressure < 6 || Pressure > 14)
            {
                Console.WriteLine($"ALERT: Unstable Pressure ({Pressure:F2} Bar)");
            }
        }
    }
}
