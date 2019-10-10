using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace FacilityMonitoring.DesktopClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public HubConnection generatorConnection;
        public HubConnection boxConnection;
        public HubConnection nh3Connection;

        public MainWindow() {
            InitializeComponent();
            generatorConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/hubs/generator").Build();
            boxConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/hubs/monitor").Build();
            nh3Connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/hubs/controller").Build();
            generatorConnection.Closed += this.Connection_Closed;
            boxConnection.Closed += this.Connection_Closed_Box;
            nh3Connection.Closed += Connection_Closed_NH3;
        }

        private async Task Connection_Closed(Exception arg) {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await generatorConnection.StartAsync();
        }

        private async Task Connection_Closed_Box(Exception arg) {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await boxConnection.StartAsync();
        }

        private async Task Connection_Closed_NH3(Exception arg) {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await this.nh3Connection.StartAsync();
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e) {
            generatorConnection.On<string>("SendGeneratorReading", (message) => {
                this.Dispatcher.Invoke(() => {
                    var newMessage = message;
                    messagesList.Items.Add(newMessage);
                });
            });

            generatorConnection.On<string>("RecieveMessage", (message) => {
                this.Dispatcher.Invoke(() => {
                    var newMessage = message;
                    messagesList.Items.Add(newMessage);
                });
            });

            this.generatorConnection.On<string>("RecieveErrorMessage", (message) => {
                this.Dispatcher.Invoke(() => {
                    this.messagesList.Items.Add(message);
                });
            });

            this.boxConnection.On<string>("RecieveAutoReading", (message) => {
                this.Dispatcher.Invoke(() => {
                    var newMessage = message;
                    this.monitorBoxList.Items.Add(newMessage);
                });
            });

            this.boxConnection.On<string>("RecieveErrorMessage", (message) => {
                this.Dispatcher.Invoke(() => {
                    this.monitorBoxList.Items.Add(message);
                });
            });


            this.nh3Connection.On<string>("RecieveAutoReading", (message) => {
                this.Dispatcher.Invoke(() => {
                    var newMessage = message;
                    this.nhBoxList.Items.Add(newMessage);
                });
            });

            this.nh3Connection.On<string>("RecieveErrorMessage", (message) => {
                this.Dispatcher.Invoke(() => {
                    this.nhBoxList.Items.Add(message);
                });
            });

            try {
                await generatorConnection.StartAsync();
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                this.getReading.IsEnabled = true;
            } catch (Exception ex) {
                messagesList.Items.Add(ex.Message);
            }

            try {
                await boxConnection.StartAsync();
                monitorBoxList.Items.Add("Connection started");
            } catch (Exception ex) {
                monitorBoxList.Items.Add(ex.Message);
            }

            try {
                await this.nh3Connection.StartAsync();
                this.nhBoxList.Items.Add("Connection started");
            } catch (Exception ex) {
                this.nhBoxList.Items.Add(ex.Message);
            }
        }

        private async void getReading_Click(object sender, RoutedEventArgs e) {
            try {
                await generatorConnection.InvokeAsync("GetGeneratorReading", "Generator 1");
                await generatorConnection.InvokeAsync("GetGeneratorReading", "Generator 2");
                await generatorConnection.InvokeAsync("GetGeneratorReading", "Generator 3");
            } catch (Exception ex) {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
