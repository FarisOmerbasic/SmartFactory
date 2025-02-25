using System;
using System.Collections.Generic;
using SmartFactoryBackend.Sensors;

namespace SmartFactoryBackend.Machines
{
    public class Machine
    {
        public string MachineId { get; set; }
        public string MachineType { get; set; }
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; }
        public bool IsOperational { get; set; }
        public bool IsActive { get; set; } 

        public Machine(string machineId, string machineType, string roomName)
        {
            MachineId = machineId;
            MachineType = machineType;
            RoomName = roomName;
            Sensors = new List<Sensor>();
            IsOperational = true; 
            IsActive = true; 
        }

        public void AddSensor(Sensor sensor)
        {
            if (sensor == null)
            {
                throw new ArgumentNullException(nameof(sensor), "Sensor cannot be null.");
            }
            Sensors.Add(sensor);
            Console.WriteLine($"Added {sensor.Name} sensor to {MachineType} (ID: {MachineId}).");
        }

        public void CheckMachineStatus()
        {
            if (!IsActive)
            {
                Console.WriteLine($"{MachineType} (ID: {MachineId}) in {RoomName} is currently off.");
                return;
            }

            bool allSensorsOperational = true;

            foreach (var sensor in Sensors)
            {
                try
                {
                    double value = sensor.GetSensorValue();
                    if (value < sensor.LowerBound || value > sensor.UpperBound)
                    {
                        Console.WriteLine($"Warning: {sensor.Name} on {MachineType} in {RoomName} exceeded safe range! (Value: {value})");
                        allSensorsOperational = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading {sensor.Name} sensor on {MachineType} (ID: {MachineId}): {ex.Message}");
                    allSensorsOperational = false;
                }
            }

            IsOperational = allSensorsOperational;

            if (IsOperational)
            {
                Console.WriteLine($"{MachineType} (ID: {MachineId}) in {RoomName} is operating normally.");
            }
            else
            {
                Console.WriteLine($"{MachineType} (ID: {MachineId}) in {RoomName} requires maintenance!");
                TriggerMaintenanceAlert();
            }
        }

        public void TurnOn()
        {
            if (IsActive)
            {
                Console.WriteLine($"{MachineType} (ID: {MachineId}) is already on.");
                return;
            }

            IsActive = true;
            Console.WriteLine($"{MachineType} (ID: {MachineId}) has been turned on.");
        }

        public void TurnOff()
        {
            if (!IsActive)
            {
                Console.WriteLine($"{MachineType} (ID: {MachineId}) is already off.");
                return;
            }

            IsActive = false;
            Console.WriteLine($"{MachineType} (ID: {MachineId}) has been turned off.");
        }

        private void TriggerMaintenanceAlert()
        {
  
            Console.WriteLine($"ALERT: Maintenance required for {MachineType} (ID: {MachineId}) in {RoomName}!");
        }

        public void DisplayMachineDetails()
        {
            Console.WriteLine($"--- Machine Details ---");
            Console.WriteLine($"Machine ID: {MachineId}");
            Console.WriteLine($"Type: {MachineType}");
            Console.WriteLine($"Room: {RoomName}");
            Console.WriteLine($"Status: {(IsOperational ? "Operational" : "Requires Maintenance")}");
            Console.WriteLine($"Active: {(IsActive ? "On" : "Off")}");
            Console.WriteLine($"Sensors:");

            foreach (var sensor in Sensors)
            {
                Console.WriteLine($"- {sensor.Name}: {sensor.GetSensorValue()} (Safe Range: {sensor.LowerBound} to {sensor.UpperBound})");
            }
        }
    }
}