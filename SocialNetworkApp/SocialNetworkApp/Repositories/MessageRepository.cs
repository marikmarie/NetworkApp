using SocialNetworkApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Repositories
{
    internal class MessageRepository
    {
 
            private List<Message> messages = new List<Message>();

            public void AddMessage(Message message)
            {
                messages.Add(message);
            }

            public List<Message> GetMessagesByUser(User user)
            {
                return messages.Where(m => m.Sender == user || m.Receiver == user).ToList();
            }
        }
    }

