using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkApp.Memory;
using SocialNetworkApp.Classes;

namespace SocialNetworkApp
{
    internal class Network
    {
        public UserMemory userMemory;
        public FriendshipMemory friendshipMemory;
        public MessageMemory messageMemory;

        public Network()
        {
            userMemory = new UserMemory();
            friendshipMemory = new FriendshipMemory();
            messageMemory = new MessageMemory();
        }

        public void SignUp(string userName, string email, string password)
        {
            if (userMemory.GetUserByEmail(email) == null)
            {
                User newUser = new User(userName, email, password);
                userMemory.AddUser(newUser);
                Console.WriteLine("Account created successfully.");
            }
            else
            {
                Console.WriteLine("Email is already registered.");
            }
        }

        public User Login(string email, string password)
        {
            User user = userMemory.GetUserByEmail(email);
            if (user != null && user.Password == password)
            {
                Console.WriteLine("Login successful.");
                return user;
            }
            else
            {
                Console.WriteLine("Invalid email or password.");
                return null;
            }
        }

        public void Logout(User user)
        {
            Console.WriteLine($"User {user.UserName} logged out.");
        }

        public void SendFriendRequest(User requester, User receiver)
        {
            if (requester != receiver && !requester.Friends.Contains(receiver))
            {
                Friendship friendship = new Friendship(requester, receiver);
                friendshipMemory.Addfriendship(friendship);
                Console.WriteLine("Friend request sent.");
            }
        }

        public void AcceptFriendRequest(User receiver, User requester)
        {
            Friendship friendship = friendshipMemory.Getfriendships(requester, receiver);
            if (friendship != null && !friendship.IsAccepted)
            {
                friendship.IsAccepted = true;
                requester.AddFriend(receiver);
                Console.WriteLine("Friend request accepted.");
            }
        }

        public void DeclineFriendRequest(User receiver, User requester)
        {
            Friendship friendship = friendshipMemory.Getfriendships(requester, receiver);
            if (friendship != null && !friendship.IsAccepted)
            {
                friendshipMemory.Removefriendship(friendship);
                Console.WriteLine("Friend request declined.");
            }
        }

        public void SendMessage(User sender, User receiver, string content)
        {
            if (sender.Friends.Contains(receiver))
            {
                Message message = new Message(Guid.NewGuid().ToString(), sender, receiver, content, DateTime.Now);
                messageMemory.AddMessage(message);
                Console.WriteLine("Message sent.");
            }
            else
            {
                Console.WriteLine("You can only send messages to friends.");
            }
        }

        public void ViewFriendsActivities(User user)
        {
            foreach (var friend in user.Friends)
            {
                Console.WriteLine($"{friend.UserName}'s Activities:");
                var messages = messageMemory.GetMessagesByUser(friend);
                foreach (var message in messages)
                {
                    Console.WriteLine($"{message.Sender.UserName} to {message.Receiver.UserName}: {message.Content} at {message.SentAt}");
                }
            }
        }

        public List<User> GetRecommendedUsers(User user)
        {
            var recommendedUsers = new Dictionary<User, int>();

            foreach (var otherUser in userMemory.GetAllUsers())
            {
                if (otherUser != user && !user.Friends.Contains(otherUser))
                {
                    int commonFriends = user.Friends.Intersect(otherUser.Friends).Count();
                    int commonInterests = user.Interests.Intersect(otherUser.Interests).Count();
                    int score = commonFriends + commonInterests;

                    if (score > 0)
                    {
                        recommendedUsers[otherUser] = score;
                    }
                }
            }

            return recommendedUsers.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).Take(10).ToList();
        }

        public List<User> GetPopularUsers()
        {
            return userMemory.GetAllUsers().OrderByDescending(u => u.Friends.Count).Take(10).ToList();
        }

        public List<User> GetShortestPath(User user1, User user2)
        {
            return new List<User>();
        }
    }
}
