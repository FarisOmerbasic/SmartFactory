using System;
using System.Threading.Tasks;
using DotNetEnv;
using SmartFactoryBackend.Models;

public class Program
{
    static async Task Main()
    {

        Env.Load("C:\\Users\\Lenovo\\source\\repos\\FarisOmerbasic\\SmartFactory\\env");


        var stringNeki = Env.GetString("TOKEN");
        Console.WriteLine(stringNeki);

        ControlRoom controlRoom = new ControlRoom("ControlRoom");
        await controlRoom.FetchSensorData();
        controlRoom.DisplaySensorData();
    }
}
