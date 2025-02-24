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

        BreakRoom breakRoom = new BreakRoom("Employee Break Room");

        await breakRoom.FetchSensorData();

        breakRoom.DisplaySensorData();

        ControlRoom controlRoom = new ControlRoom("Factory Operations Center");
        await controlRoom.FetchSensorData();
        controlRoom.DisplaySensorData();
    }
}
