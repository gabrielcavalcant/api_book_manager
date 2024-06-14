using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace book_mvc.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public bool Emprestimo { get; set; }
    }
}
