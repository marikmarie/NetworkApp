using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Models
{
    internal class FriendShip
    {
        
            public User Requester { get; private set; }
            public User Receiver { get; private set; }
            public DateTime RequestedAt { get; private set; }
            public bool IsAccepted { get; set; }

            public Friendship(User requester, User receiver)
            {
                Requester = requester;
                Receiver = receiver;
                RequestedAt = DateTime.Now;
                IsAccepted = false;
            }
        }
    }

}
}
