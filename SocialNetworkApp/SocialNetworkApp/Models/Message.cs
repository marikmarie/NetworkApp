using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Models
{
    internal class Message
    {
  
            public string Id { get; private set; }
            public User Sender { get; private set; }
            public User Receiver { get; private set; }
            public string Content { get; private set; }
            public DateTime SentAt { get; private set; }

            public Message(string id, User sender, User receiver, string content, DateTime sentAt)
            {
                Id = id;
                Sender = sender;
                Receiver = receiver;
                Content = content;
                SentAt = sentAt;
            }
        }
    }

}
}
