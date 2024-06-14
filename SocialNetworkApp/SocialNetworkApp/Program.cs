using SocialNetworkApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestData.RunTests();
            Network network = new Network();
            User currentUser = null;

            while (true)
            {
                if (currentUser == null)
                {
                    Console.Clear();
                    Console.WriteLine("********************************************************");
                    Console.WriteLine("\t-------Welcome to our Social Network--------");
                    Console.WriteLine("********************************************************");
                    Console.WriteLine("\t1. Register");
                    Console.WriteLine("\t2. Login");
                    Console.WriteLine("\t3. Exit\n");
                    Console.Write("Choose an option: ");
                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            Console.Write("Enter username: ");
                            string userName = Console.ReadLine();
                            Console.Write("Enter email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter password: ");
                            string password = Console.ReadLine();
                            network.SignUp(userName, email, password);
                            break;
                        case "2":
                            Console.Write("Enter email: ");
                            email = Console.ReadLine();
                            Console.Write("Enter password: ");
                            password = Console.ReadLine();
                            currentUser = network.Login(email, password);
                            if (currentUser != null)
                            {
                                Console.WriteLine("Login successful.");
                                DisplayInterestsMenu(currentUser);
                            }
                            break;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, {currentUser.UserName}");
                    Console.WriteLine("1. Add Friend");
                    Console.WriteLine("2. Accept Friend Request");
                    Console.WriteLine("3. Decline Friend Request");
                    Console.WriteLine("4. View Friend Requests");
                    Console.WriteLine("5. Send Message");
                    Console.WriteLine("6. View Received Messages");
                    Console.WriteLine("7. View Friends' Activities");
                    Console.WriteLine("8. Recommend Users");
                    Console.WriteLine("9. View Popular Users");
                    Console.WriteLine("10. Join Social Network");
                    Console.WriteLine("11. Logout");
                    Console.Write("Choose an option: ");
                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            SendFriendRequestMenu(network, currentUser);
                            break;
                        case "2":
                            AcceptFriendRequestMenu(network, currentUser);
                            break;
                        case "3":
                            DeclineFriendRequestMenu(network, currentUser);
                            break;
                        case "4":
                            ViewFriendRequestsMenu(network, currentUser);
                            break;
                        case "5":
                            SendMessageMenu(network, currentUser);
                            break;
                        case "6":
                            ViewReceivedMessagesMenu(network, currentUser);
                            break;
                        case "7":
                            network.ViewFriendsActivities(currentUser);
                            break;
                        case "8":
                            RecommendUsersMenu(network, currentUser);
                            break;
                        case "9":
                            ViewPopularUsersMenu(network);
                            break;
                        case "10":
                            JoinSocialNetworkMenu(network, currentUser);
                            break;
                        case "11":
                            network.Logout(currentUser);
                            currentUser = null;
                            break;
                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayInterestsMenu(User user)
        {
            var interests = new List<string> { "Sports", "Technology", "Religion", "Business" };
            Console.WriteLine("Select your interests (comma separated):");
            for (int i = 0; i < interests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {interests[i]}");
            }
            string selectedInterests = Console.ReadLine();
            var selectedInterestsList = selectedInterests.Split(',').Select(i => i.Trim()).ToList();

            foreach (var interest in selectedInterestsList)
            {
                if (int.TryParse(interest, out int index) && index >= 1 && index <= interests.Count)
                {
                    user.AddInterest(interests[index - 1]);
                }
            }

            Console.WriteLine("Your interests:");
            foreach (var interest in user.Interests)
            {
                Console.WriteLine($"- {interest}");
            }
        }


        static void SendFriendRequestMenu(Network network, User currentUser)
        {
            var usersToAdd = network.userMemory.GetAllUsers().Where(u => u != currentUser && !currentUser.Friends.Contains(u)).ToList();
            if (usersToAdd.Any())
            {
                Console.WriteLine("Select a user to send a friend request:");
                for (int i = 0; i < usersToAdd.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {usersToAdd[i].UserName}");
                }
                int friendIndex = int.Parse(Console.ReadLine()) - 1;
                if (friendIndex >= 0 && friendIndex < usersToAdd.Count)
                {
                    network.SendFriendRequest(currentUser, usersToAdd[friendIndex]);
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("No users available to send friend requests.");
            }
        }

        static void AcceptFriendRequestMenu(Network network, User currentUser)
        {
            var friendRequests = network.friendshipMemory.GetfriendshipsByUser(currentUser).Where(f => !f.IsAccepted && f.Receiver == currentUser).ToList();
            if (friendRequests.Any())
            {
                Console.WriteLine("Select a friend request to accept:");
                for (int i = 0; i < friendRequests.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {friendRequests[i].Requester.UserName}");
                }
                int requestIndex = int.Parse(Console.ReadLine()) - 1;
                if (requestIndex >= 0 && requestIndex < friendRequests.Count)
                {
                    network.AcceptFriendRequest(currentUser, friendRequests[requestIndex].Requester);
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("No friend requests to accept.");
            }
        }

        static void DeclineFriendRequestMenu(Network network, User currentUser)
        {
            var declineRequests = network.friendshipMemory.GetfriendshipsByUser(currentUser).Where(f => !f.IsAccepted && f.Receiver == currentUser).ToList();
            if (declineRequests.Any())
            {
                Console.WriteLine("Select a friend request to decline:");
                for (int i = 0; i < declineRequests.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {declineRequests[i].Requester.UserName}");
                }
                int declineIndex = int.Parse(Console.ReadLine()) - 1;
                if (declineIndex >= 0 && declineIndex < declineRequests.Count)
                {
                    network.DeclineFriendRequest(currentUser, declineRequests[declineIndex].Requester);
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("No friend requests to decline.");
            }
        }

        static void ViewFriendRequestsMenu(Network network, User currentUser)
        {
            var pendingRequests = network.friendshipMemory.GetfriendshipsByUser(currentUser).Where(f => !f.IsAccepted && f.Receiver == currentUser).ToList();
            if (pendingRequests.Any())
            {
                Console.WriteLine("Pending Friend Requests:");
                foreach (var request in pendingRequests)
                {
                    Console.WriteLine($"{request.Requester.UserName} sent you a friend request.");
                }
            }
            else
            {
                Console.WriteLine("You have no pending friend requests.");
            }
        }

        static void SendMessageMenu(Network network, User currentUser)
        {
            var friendsToMessage = currentUser.Friends;
            if (friendsToMessage.Any())
            {
                Console.WriteLine("Select a friend to message:");
                for (int i = 0; i < friendsToMessage.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {friendsToMessage[i].UserName}");
                }
                int messageIndex = int.Parse(Console.ReadLine()) - 1;
                if (messageIndex >= 0 && messageIndex < friendsToMessage.Count)
                {
                    Console.Write("Enter your message: ");
                    string messageContent = Console.ReadLine();
                    network.SendMessage(currentUser, friendsToMessage[messageIndex], messageContent);
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("No friends available to send messages.");
            }
        }

        static void ViewReceivedMessagesMenu(Network network, User currentUser)
        {
            var receivedMessages = network.messageMemory.GetMessagesByUser(currentUser).Where(m => m.Receiver == currentUser).ToList();
            if (receivedMessages.Any())
            {
                Console.WriteLine("Messages sent to you:");
                foreach (var message in receivedMessages)
                {
                    Console.WriteLine($"{message.Sender.UserName} to you: {message.Content} at {message.SentAt}");
                }
            }
            else
            {
                Console.WriteLine("No messages received.");
            }
        }

        static void RecommendUsersMenu(Network network, User currentUser)
        {
            var recommendedUsers = network.GetRecommendedUsers(currentUser);
            Console.WriteLine("Recommended Users:");
            foreach (var recommendedUser in recommendedUsers)
            {
                Console.WriteLine(recommendedUser.UserName);
            }
        }

        static void ViewPopularUsersMenu(Network network)
        {
            var popularUsers = network.GetPopularUsers();
            Console.WriteLine("Most Popular Users:");
            foreach (var popularUser in popularUsers)
            {
                Console.WriteLine($"{popularUser.UserName} with {popularUser.Friends.Count} friends");
            }
        }

        static void JoinSocialNetworkMenu(Network network, User currentUser)
        {
            Console.WriteLine("Join Social Network based on:");
            Console.WriteLine("1. Friends' Networks");
            Console.WriteLine("2. Your Interests");
            Console.Write("Choose an option: ");
            string joinOption = Console.ReadLine();
            if (joinOption == "1")
            {
                Console.WriteLine("Friends' Networks:");
                var friendsNetworks = currentUser.Friends.SelectMany(f => f.SocialNetworks).Distinct().ToList();
                for (int i = 0; i < friendsNetworks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {friendsNetworks[i]}");
                }
                Console.Write("Enter the number of the network to join: ");
                if (int.TryParse(Console.ReadLine(), out int networkNumber) && networkNumber >= 1 && networkNumber <= friendsNetworks.Count)
                {
                    currentUser.JoinSocialNetwork(friendsNetworks[networkNumber - 1]);
                    Console.WriteLine("You have joined the network.");
                }
                else
                {
                    Console.WriteLine("Invalid network number.");
                }
            }
            else if (joinOption == "2")
            {
                Console.WriteLine("Interests:");
                var interests = new List<string> { "Sports", "Technology", "Religion", "Business" };
                for (int i = 0; i < interests.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {interests[i]}");
                }
                Console.Write("Enter the number of the interest to join networks: ");
                if (int.TryParse(Console.ReadLine(), out int interestNumber) && interestNumber >= 1 && interestNumber <= interests.Count)
                {
                    var socialNetworks = GetSocialNetworksByInterest(interests[interestNumber - 1]);
                    Console.WriteLine("Social Networks:");
                    for (int i = 0; i < socialNetworks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {socialNetworks[i]}");
                    }
                    Console.Write("Enter the number of the social network to join: ");
                    if (int.TryParse(Console.ReadLine(), out int networkNumber) && networkNumber >= 1 && networkNumber <= socialNetworks.Count)
                    {
                        currentUser.JoinSocialNetwork(socialNetworks[networkNumber - 1]);
                        Console.WriteLine("You have joined the network.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid network number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid interest number.");
                }
            }
        } 
        static List<string> GetSocialNetworksByInterest(string interest)
        {
            var socialNetworks = new Dictionary<string, List<string>>
            {
                { "Sports", new List<string> { "Spotify", "FanZone", "AthleteNet", "GameTime" } },
                { "Technology", new List<string> { "TechTalk", "CodeHub", "GadgetWorld", "Innovate" } },
                { "Religion", new List<string> { "FaithBook", "SoulConnect", "Believer's Hub", "SpiritualityNet" } },
                { "Business", new List<string> { "BizLink", "Entrepreneur's Network", "MarketSpace", "CorporateHub" } }
            };

            return socialNetworks.ContainsKey(interest) ? socialNetworks[interest] : new List<string>();
        }
    }
}
