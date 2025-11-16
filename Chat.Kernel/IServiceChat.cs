using System.ServiceModel;

namespace Chat.Kernel
{
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        // Здесь не нужно дожидаться ответа от сервера
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int id);
    }
}
