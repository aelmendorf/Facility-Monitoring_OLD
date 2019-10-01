using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Console_Table;
using FacilityMonitoring.Common.Server;
using Microsoft.AspNetCore.SignalR.Client;

namespace FacilityMonitoring.ConsoleTesting {
    class Program2 {
        static async Task Main(string[] args) {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://172.20.1.61:443/hubs/monitor")
                .Build();


            connection.On<WebData>(Strings.Events.ReadingSent,(data) => {
                ConsoleTable table = new ConsoleTable();
                table.Columns.Add(data.Headers);
                List<string> cell = new List<string>();
                foreach(var item in data.Row) {
                    cell.Add(item);
                }
                table.Rows.Add(cell.ToArray());
                Console.WriteLine(table.ToString());

            });

            // Loop is here to wait until the server is running
            while (true) {
                try {
                    await connection.StartAsync();
                    break;
                } catch {
                    await Task.Delay(1000);
                }
            }

            Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            Console.ReadLine();
        }
    }
}
