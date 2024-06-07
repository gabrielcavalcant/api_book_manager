using System;
using System.Collections.Generic;
using System.Linq;
using book_manager.Models;

namespace book_manager.Repository
{
    public class UserRepository
    {
        public static User Get(string Username, string Password)
        {
            var users = new List<User>()
            {
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    Role = "123",
                    isAuthorized = true,
                },
                new User
                {
                    Id = 2,
                    Username = "Ken",
                    Password = "123",
                    Role = "123",
                    isAuthorized = true,
                },
                new User
                {
                    Id = 3,
                    Username = "Ken",
                    Password = "123",
                    Role = "123",
                    isAuthorized = true,
                }
            };
                return users.FirstOrDefault();
        }
    }
}
