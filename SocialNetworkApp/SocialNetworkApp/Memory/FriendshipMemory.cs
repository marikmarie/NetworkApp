using SocialNetworkApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Memory
{
    internal class FriendshipMemory
    {
        private List<Friendship> friendships = new List<Friendship>();

        public void Addfriendship(Friendship friendship)
        {
            friendships.Add(friendship);
        }

        public void Removefriendship(Friendship friendship)
        {
            friendships.Remove(friendship);
        }
        public List<Friendship> GetfriendshipsByUser(User user)
        {
            return friendships.Where(f => f.Requester == user ||  f.Receiver == user).ToList();
        }
        public Friendship Getfriendships(User requester, User receiver) {
            return friendships.FirstOrDefault(f => f.Requester == requester && f.Receiver == receiver);
        }
    }
}
