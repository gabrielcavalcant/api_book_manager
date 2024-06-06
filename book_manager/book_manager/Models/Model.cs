using System;
using System.ComponentModel.DataAnnotations;

namespace book_manager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isAuthorized { get; set; }
    }



    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public bool Emprestimo { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}