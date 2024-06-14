using SocialNetworkApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Repositories
{
    internal class FriendshipRepository
    {

            private List<Friendship> friendships = new List<Friendship>();

            public void AddFriendship(Friendship friendship)
            {
                friendships.Add(friendship);
            }

            public void RemoveFriendship(Friendship friendship)
            {
                friendships.Remove(friendship);
            }

            public List<Friendship> GetFriendshipsByUser(User user)
            {
                return friendships.Where(f => f.Requester == user || f.Receiver == user).ToList();
            }

            public Friendship GetFriendship(User requester, User receiver)
            {
                return friendships.FirstOrDefault(f => f.Requester == requester && f.Receiver == receiver);
            }
        }
    }

