using System.Collections.Generic;

namespace Arquitetura.Services.Validator.Notification
{
    public interface INotification
    {
        bool HasNotification();
        List<Message> GetNotifications();
        void Handle(Message message);
    }
}
