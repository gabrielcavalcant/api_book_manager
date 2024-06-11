using System.Data;
using System.Data.SqlClient;
using book_manager.Models;
using book_manager.Repository;

namespace book_manager.Business
{
    public class Business
    {
        private readonly ImpRepository _repository;

        public Business()
        {
            _repository = new ImpRepository();
        }

        public DataTable GetBooks()
        {
            return _repository.ExecuteProcedure("[dbo].[prGET_BOOKS]", null);
        }

        public DataTable AddBook(Livro livro)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("Titulo", livro.Titulo),
                new SqlParameter("Autor", livro.Autor),
                new SqlParameter("Genero", livro.Genero),
                new SqlParameter("Emprestimo", livro.Emprestimo),
            };
            return _repository.ExecuteProcedure("[dbo].[prADD_BOOK]", parameters);
        }
         public DataTable GetLivrosByGeneroAndTitulo(string genero, string titulo)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("Genero", genero),
                    new SqlParameter("Titulo", titulo)
                };
                return _repository.ExecuteProcedure("[dbo].[prGET_BOOKS_BY_GENERO_AND_TITULO]", parameters);
            }

        public DataTable UpdateBook(int id, Livro livro)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("Id", livro.Id),
                new SqlParameter("Titulo", livro.Titulo),
                new SqlParameter("Autor", livro.Autor),
                new SqlParameter("Genero", livro.Genero),
                new SqlParameter("Emprestimo", livro.Emprestimo),
            };
            return _repository.ExecuteProcedure("[dbo].[prUPDATE_BOOK]", parameters);
        }

        public DataTable DeleteBook(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("Id", id),
            };
            return _repository.ExecuteProcedure("[dbo].[prDELETE_BOOK]", parameters);
        }
    }
}