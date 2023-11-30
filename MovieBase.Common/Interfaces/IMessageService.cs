namespace MovieBase.Common.Interfaces;
public interface IMessageService
{
    Task<bool> Connect();

    event Action<string> OnMessage;
    Task Disconnect();
}
