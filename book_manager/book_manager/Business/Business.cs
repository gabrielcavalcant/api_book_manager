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

        public void AddBook(Livro livro)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("Titulo", livro.Titulo),
                new SqlParameter("Autor", livro.Autor),
                new SqlParameter("Genero", livro.Genero),
                new SqlParameter("Emprestimo", livro.Emprestimo),
            };
            _repository.ExecuteNonQueryProcedure("[dbo].[prADD_BOOK]", parameters);
        }

        public void UpdateBook(Livro livro)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("Id", livro.Id),
                new SqlParameter("Titulo", livro.Titulo),
                new SqlParameter("Autor", livro.Autor),
                new SqlParameter("Genero", livro.Genero),
                new SqlParameter("Emprestimo", livro.Emprestimo),
            };
            _repository.ExecuteNonQueryProcedure("[dbo].[prUPDATE_BOOK]", parameters);
        }

        public void DeleteBook(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("Id", id),
            };
            _repository.ExecuteNonQueryProcedure("[dbo].[prDELETE_BOOK]", parameters);
        }
    }
}
