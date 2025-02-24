using System;
using System.Threading.Tasks;
using SmartFactoryBackend.Models;

class Program
{
    static async Task Main()
    {
        // Kreiranje instance prostorije "BreakRoom"
        BreakRoom breakRoom = new BreakRoom("Employee Break Room");

        // Dohvati podatke sa API-ja
        await breakRoom.FetchSensorData();

        // Prikazi podatke o senzorima
        breakRoom.DisplaySensorData();
    }
}
