using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitetura.Services.Validator.Notification
{
    public interface INotification
    {
        bool HasNotification();
        List<Message> GetNotifications();
        void Handle(Message message);
    }
}
