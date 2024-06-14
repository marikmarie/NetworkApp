﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Models
{
    internal class User
    {
  
            private static int _nextId = 1;

            public int UserId { get; private set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<User> Friends { get; private set; }
            public List<string> Interests { get; private set; }

            public User(string userName, string email, string password)
            {
                UserId = _nextId++;
                UserName = userName;
                Email = email;
                Password = password;
                Friends = new List<User>();
                Interests = new List<string>();
            }

            public void AddFriend(User user)
            {
                if (!Friends.Contains(user))
                {
                    Friends.Add(user);
                    user.Friends.Add(this); // Mutual friendship
                }
            }

            public void RemoveFriend(User user)
            {
                if (Friends.Contains(user))
                {
                    Friends.Remove(user);
                    user.Friends.Remove(this); // Remove mutual friendship
                }
            }

            public void AddInterest(string interest)
            {
                if (!Interests.Contains(interest))
                {
                    Interests.Add(interest);
                }
            }

            public void RemoveInterest(string interest)
            {
                if (Interests.Contains(interest))
                {
                    Interests.Remove(interest);
                }
            }
        }
    }
