using SocialNetworkApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp
{
    internal class TestData
    {
 
            public static void RunTests()
            {
                TestUserOperations();
                TestFriendshipOperations();
                TestNetworkOperations();
            }

            private static void TestUserOperations()
            {
                User user1 = new User("Mariam", "mm", "123");
                User user2 = new User("Kevin", "kev", "123");

                // Test adding and removing friends
                user1.AddFriend(user2);
                if (!user1.Friends.Contains(user2) || !user2.Friends.Contains(user1))
                {
                    Console.WriteLine("TestUserOperations: Adding friends failed.");
                }

                user1.RemoveFriend(user2);
                if (user1.Friends.Contains(user2) || user2.Friends.Contains(user1))
                {
                    Console.WriteLine("TestUserOperations: Removing friends failed.");
                }

                // Test adding and removing interests
                user1.AddInterest("Sports");
                user1.AddInterest("Technology");
                if (!user1.Interests.Contains("Sports") || !user1.Interests.Contains("Technology"))
                {
                    Console.WriteLine("TestUserOperations: Adding interests failed.");
                }

                user1.RemoveInterest("Sports");
                if (user1.Interests.Contains("Sports"))
                {
                    Console.WriteLine("TestUserOperations: Removing interests failed.");
                }
            }

            private static void TestFriendshipOperations()
            {
                User user1 = new User("Mariam", "mm", "123");
                User user2 = new User("Kevin", "kev", "123");

                Friendship friendship = new Friendship(user1, user2);
                if (friendship.Requester != user1 || friendship.Receiver != user2 || friendship.IsAccepted)
                {
                    Console.WriteLine("TestFriendshipOperations: Creating friendship failed.");
                }

                friendship.IsAccepted = true;
                if (!friendship.IsAccepted)
                {
                    Console.WriteLine("TestFriendshipOperations: Accepting friendship failed.");
                }
            }

            private static void TestNetworkOperations()
            {
                Network network = new Network();

                // Test user sign up and login
                network.SignUp("Mariam", "mm", "123");
                User mm = network.Login("mm", "123");
                if (mm == null || mm.UserName != "Alice")
                {
                    Console.WriteLine("TestNetworkOperations: User sign up or login failed.");
                }

                network.SignUp("Kevin", "kev", "123");
                User kevin = network.Login( "kev", "123");

                // Test sending and accepting friend requests
                network.SendFriendRequest(mm, kevin);
                var friendship = network.friendshipMemory.Getfriendships(mm, kevin);
                if (friendship == null)
                {
                    Console.WriteLine("TestNetworkOperations: Sending friend request failed.");
                }

                network.AcceptFriendRequest(mm, kevin);
                if (!friendship.IsAccepted || !mm.Friends.Contains(kevin) || !kevin.Friends.Contains(mm))
                {
                    Console.WriteLine("TestNetworkOperations: Accepting friend request failed.");
                }

                // Test sending and receiving messages
                network.SendMessage(mm, kevin, "Hello, Bob!");
                var messages = network.messageMemory.GetMessagesSentToUser(kevin);
                if (!messages.Any(m => m.Sender == mm && m.Receiver == kevin && m.Content == "Hello, Bob!"))
                {
                    Console.WriteLine("TestNetworkOperations: Sending or receiving messages failed.");
                }

                // Test viewing friend requests
                network.SignUp("Charlie", "charlie", "123");
                User charlie = network.Login("charlie", "123");
                network.SendFriendRequest(mm, charlie);
                var friendRequests = network.friendshipMemory.GetfriendshipsByUser(charlie).Where(f => !f.IsAccepted && f.Receiver == charlie).ToList();
                if (friendRequests.Count != 1 || friendRequests[0].Requester != mm)
                {
                    Console.WriteLine("TestNetworkOperations: Viewing friend requests failed.");
                }
            }
        }
    }
