using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace FacilityMonitoring.DesktopClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public HubConnection connection;
        public MainWindow() {
            InitializeComponent();
            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/hubs/generator").Build();
            connection.Closed += this.Connection_Closed;
        }

        private async Task Connection_Closed(Exception arg) {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e) {
            connection.On<string>("SendGeneratorReading", (message) => {
                this.Dispatcher.Invoke(() => {
                    var newMessage = message;
                    messagesList.Items.Add(newMessage);
                });
            });

            connection.On<string>("RecieveMessage", (message) => {
                this.Dispatcher.Invoke(() => {
                    var newMessage = message;
                    messagesList.Items.Add(newMessage);
                });
            });

            this.connection.On<string>("RecieveErrorMessage", (message) => {
                this.Dispatcher.Invoke(() => {
                    this.messagesList.Items.Add(message);
                });
            });

            try {
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                this.getReading.IsEnabled = true;
            } catch (Exception ex) {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void getReading_Click(object sender, RoutedEventArgs e) {
            try {
                await connection.InvokeAsync("GetGeneratorReading",1);
                await connection.InvokeAsync("GetGeneratorReading", 2);
                await connection.InvokeAsync("GetGeneratorReading", 3);
            } catch (Exception ex) {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
