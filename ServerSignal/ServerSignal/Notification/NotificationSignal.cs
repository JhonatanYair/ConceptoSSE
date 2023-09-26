using Microsoft.AspNetCore.SignalR.Client;

namespace ServerSignal.NotificationSignal
{
    public class NotificationSignal
    {

        public NotificationSignal() 
        {

        }

        public async Task NotifyASignalRGroup(string groupName, string mensaje)
        {
            var hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7242/SSEHub")
            .Build();
            Console.WriteLine();

            try
            {
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("SendMessageToGroup", groupName, mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
            }
        }

        public async Task NotifyASignalRPrivate(string userId, string mensaje)
        {
            var hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7242/SSEHub")
            .Build();
            Console.WriteLine();

            try
            {
                await hubConnection.StartAsync();
                //await hubConnection.InvokeAsync("SendPrivateMessage", userId, mensaje);
                await hubConnection.InvokeAsync("SendPrivateMessage", userId, mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
            }
        }

    }

}
