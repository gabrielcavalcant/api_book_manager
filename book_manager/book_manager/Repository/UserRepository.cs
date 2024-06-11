using System.Collections.Generic;
using System.Linq;
using book_manager.Models;

namespace book_manager.Repository
{
    public static class UserRepository
    {
        // Lista estática simulando um banco de dados
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin123", Role = "Manager" },
            new User { Id = 2, Username = "user", Password = "user123", Role = "user" }
        };

        // Método para obter um usuário pelo nome de usuário e senha
        public static User Get(string username, string password)
        {
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
