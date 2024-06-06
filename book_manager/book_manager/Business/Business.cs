using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime;
using book_manager.Repository;

namespace book_manager.Business
{
    public class Business
    {
        public Business()
        {

        }

        public DataTable GetCustumers(int id)
        {
            ImpRepository _Repository = new ImpRepository();
            SqlParameter[] parameter =
            {
                new SqlParameter("id", id),
            };
            return _Repository.ExecuteProcedure("[dbo].[prGET_CUSTUMERS]");
        }
    }


}
