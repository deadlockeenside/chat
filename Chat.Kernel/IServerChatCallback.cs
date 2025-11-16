using System.ServiceModel;

namespace Chat.Kernel
{
    public interface IServerChatCallback
    {
        // Здесь не нужно серверу дожидаться ответа от клиента
        [OperationContract(IsOneWay = true)]
        void MessageCallback(string message);
    }
}
