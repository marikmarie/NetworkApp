using SocialNetworkApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkApp.Repositories
{
    internal class UserRespository
    {
 
        public class UserRepository
        {
            private List<User> users = new List<User>();

            public void AddUser(User user)
            {
                users.Add(user);
            }

            public void RemoveUser(User user)
            {
                users.Remove(user);
            }

            public User GetUserByEmail(string email)
            {
                return users.FirstOrDefault(u => u.Email == email);
            }

            public List<User> GetAllUsers()
            {
                return users;
            }
        }
    }

}
