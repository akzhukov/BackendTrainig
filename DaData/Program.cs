using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;


namespace DaDataConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hubUrl = "https://localhost:44328/DaDataHub";
            HubConnection hubConnection = new HubConnectionBuilder().WithUrl(hubUrl).Build();
            hubConnection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine($"There was an error opening the connection: {task.Exception.GetBaseException()}");
                }
                else
                {
                    Console.WriteLine("Connection opened");
                    Console.WriteLine("Press any key to exit");

                    hubConnection.On<string>("GetCompanyName", Console.WriteLine);

                    Console.ReadLine();  
                }
            }).Wait();

            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();
        }
    }
}
