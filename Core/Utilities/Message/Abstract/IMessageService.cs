namespace Core.Utilities.Message.Abstract
{
    public interface IMessageService
    {
        Task SendMessage(string to, string subject, string message);
    }
}
