using System.Collections.Generic;
using System.Linq;

namespace Arquitetura.Services.Validator.Notification
{
    public class Notification : INotification
    {
        private List<Message> _messages;

        public Notification()
        {
            _messages = new List<Message>();
        }

        public List<Message> GetNotifications()
        {
            return _messages;
        }

        public void Handle(Message message)
        {
            _messages.Add(message);
        }

        public bool HasNotification()
        {
            return _messages.Any();
        }
    }
}
