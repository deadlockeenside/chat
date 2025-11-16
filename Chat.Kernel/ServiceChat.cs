using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Chat.Kernel
{
    // При каждом подключении обычно создается каждый новый экземпляр
    // Для онлайн чата нам нужен только один общий объект для всех, чтобы сервис знал о всех клиентах и рассылал им сообщения
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        // Нужно для системных сообщений
        // Т. к. такого ID нет в списке пользователей, то колбэк не будет отправлен в пустоту, но и сообщение доставится всем пользователям
        private const int SYSTEM_ID = 0;

        private List<User> _users = new List<User>();
        private int _nextId = 1;

        public int Connect(string name)
        {
            User user = new User
            {
                Id = _nextId++,
                Name = name,
                OperationContext = OperationContext.Current
            };

            SendMessage($" {user.Name} подключился к чату", SYSTEM_ID);
            _users.Add(user);

            return _nextId;
        }

        public void Disconnect(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user != null) 
            {
                _users.Remove(user);
                SendMessage($" {user.Name} покинул чат", SYSTEM_ID);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var user in _users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var sender = _users.FirstOrDefault(u => u.Id == id);

                if (sender != null)
                    answer += $" {user.Name}: ";

                answer += message;
                user.OperationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}
