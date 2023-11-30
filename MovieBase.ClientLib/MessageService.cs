using Microsoft.AspNetCore.SignalR.Client;
using MovieBase.Common.Interfaces;
using System.Data.Common;
using System.Diagnostics;

namespace MovieBase.ClientLib;
public class MessageService : IMessageService
{
    private HubConnection? _connection;

    public event Action<string> OnMessage = _ => { };

    public async Task<bool> Connect()
    {
        try
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7267/messages")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("Message", s =>
            {
                OnMessage?.Invoke(s);
            });

            await _connection.StartAsync();
            return true;
        }
        catch (Exception ex)
        {
            Trace.TraceError($"{ex}");
            return false;
        }
    }

    public Task Disconnect() => _connection!.StopAsync();
}
