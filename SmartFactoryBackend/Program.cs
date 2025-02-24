using System;
using System.Threading.Tasks;
using DotNetEnv;
using SmartFactoryBackend.Models;

public class Program
{
    static async Task Main()
    {
        Env.Load("C:\\Users\\User\\Documents\\GitHub\\SmartFactory\\SmartFactoryBackend\\.env");


        var stringNeki = Env.GetString("TOKEN");
        Console.WriteLine(stringNeki);

        // Kreiranje instance prostorije "BreakRoom"
        BreakRoom breakRoom = new BreakRoom("Employee Break Room");

        // Dohvati podatke sa API-ja
        await breakRoom.FetchSensorData();

        // Prikazi podatke o senzorima
        breakRoom.DisplaySensorData();
    }
}
