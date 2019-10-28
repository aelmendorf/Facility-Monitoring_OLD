using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Console_Table;

using Microsoft.Exchange.WebServices.Data;

namespace FacilityMonitoring.ConsoleTesting {
    class Program2 {
        public static async Task<int> Main(string[] args) {


            ExchangeService service=new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            WebCredentials credentials = new WebCredentials("facilityalerts", "Facility!1sskv", "sskep.com");
            service.Credentials = credentials;
            service.Url = new Uri(@"https://email.seoulsemicon.com/EWS/Exchange.asmx");
            EmailMessage message = new EmailMessage(service);
            message.ToRecipients.Add("aelmendorf@s-et.com");
            message.Subject = "Inventory Stock Alert";
            MessageBody body = new MessageBody();
            body.Text = "Everything is broken!";
            message.Body = body;
            await message.SendAndSaveCopy().ContinueWith(ret=>Console.WriteLine("Done!"));
            Console.ReadKey();

            return 1;
            //var connection = new HubConnectionBuilder()
            //    .WithUrl("http://localhost:5000/hubs/monitor")
            //    .Build();

            //connection.On<string>(Strings.Events.ReadingSent, (data) => {
            //    Console.WriteLine(data);
            //});


            //// Loop is here to wait until the server is running
            //while (true) {
            //    try {
            //        await connection.StartAsync();
            //        break;
            //    } catch {
            //        await Task.Delay(1000);
            //    }
            //}

            //Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            //Console.ReadLine();

            //var host = new HostBuilder()
            //    .ConfigureLogging(logging => {
            //        logging.AddConsole();
            //    })
            //    .ConfigureServices((services) => {
            //        services.AddHostedService<MonitorHubClient>();
            //        services.AddHostedService<GeneratorHubClient>();
            //    })
            //    .Build();

            //await host.RunAsync();
        }
    }
}
