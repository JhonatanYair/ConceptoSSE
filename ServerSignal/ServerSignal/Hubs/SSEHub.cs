using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Queues.AbstracionLayer;
using Queues.AbstracionLayer.Enums;
using System;

namespace ServerSignal.Hubs;

public class SSEHub : Hub
{
    private readonly QueueServiceBase queueService;


    public SSEHub(QueueServiceBase _queueService)
    {
        queueService= _queueService;
    }

    //public override async Task OnDisconnectedAsync()
    //{

    //}

    public async Task SendMessage(string mensaje)
    {
        Console.WriteLine(mensaje);
        await Clients.All.SendAsync("ReceivedMessage", mensaje);
    }

    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task JoinUser(string UserId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, UserId);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessageGroup", message);
    }

    public async Task SendPrivateMessage(string userId, string message)
    {
        Console.WriteLine();
        await Clients.Group(userId).SendAsync("ReceiveMessagePrivate", message);
    }

    public async Task SubsPrivateMessageQueue(string groupName, string userId)
    {

        NotificationSignal.NotificationSignal notificationSignal = new NotificationSignal.NotificationSignal();

        queueService.SubscribeToMessageReceived(async (sender, message) =>
        {
            Console.WriteLine($"{message} {userId} SSE private");
            Payload messageObject = JsonConvert.DeserializeObject<Payload>(message);

            if (userId == messageObject.To)
            {
                notificationSignal.NotifyASignalRPrivate(userId, message);
            }
        });

        if (Enum.TryParse(groupName, out RabbitExchange valorEnum))
        {
            Console.WriteLine($"El valor enum es: {valorEnum}");
            var message = queueService.ConsumeMessage(valorEnum);
        }
        else
        {
            Console.WriteLine("El string no coincide con ningún valor enum.");
        }

    }

    public async Task SubsMessageToGroupQueue(string groupName)
    {
        NotificationSignal.NotificationSignal notificationSignal = new NotificationSignal.NotificationSignal();

        queueService.SubscribeToMessageReceived(async (sender, message) =>
        {
            Console.WriteLine($"{message} {groupName} SSE grupo");
            notificationSignal.NotifyASignalRGroup(groupName, message);
        });

        if (Enum.TryParse(groupName, out RabbitExchange valueEnum))
        {
            Console.WriteLine($"El valor enum es: {valueEnum}");
            var message = queueService.ConsumeMessage(valueEnum);
        }
        else
        {
            Console.WriteLine("El string no coincide con ningún valor enum.");
        }

    }

}
