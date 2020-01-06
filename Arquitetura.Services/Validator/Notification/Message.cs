using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitetura.Services.Validator.Notification
{
    public class Message
    {
        public Message(string description)
        {
            Description = description;
        }

        public string Description { get; }
    }
}
