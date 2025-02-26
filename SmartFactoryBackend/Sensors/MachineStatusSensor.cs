namespace SmartFactoryBackend.Sensors
{
    public enum MachineState { Running, Idle, Malfunctioning }

    public class MachineStatusSensor : Sensor
    {
        public MachineState CurrentState { get; private set; }

        // Constructor now accepts AlarmTemplate
        public MachineStatusSensor(string id, AlarmTemplate alarmTemplate) : base(id, "Machine Status Sensor", alarmTemplate) { }

        // Method to simulate reading the machine status
        public void ReadData()
        {
            Random rand = new Random();
            int state = rand.Next(0, 3);  // Randomly simulate one of the 3 states
            CurrentState = (MachineState)state;

            Console.WriteLine($"{Name} ({Id}) - Current State: {CurrentState}");

            // Output the alert level based on the current machine state
            Console.WriteLine($"Machine Status Alert Level: {GetAlertLevel()}");
        }


        public override string GetAlertLevel()
        {
            switch (CurrentState)
            {
                case MachineState.Malfunctioning:
               
                    return "Critical High";
                case MachineState.Idle:
                  
                    return "Normal";
                case MachineState.Running:
                    
                    return "Normal";
                default:
                    return base.GetAlertLevel();  
            }
        }
    }
}
