using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime;

namespace book_manager.Business
{
    public class Business
    {
        public Business()
        {

        }

        public DataTable GetCustumers(int id)
        {
            Repository _Repository = new Repository();
            SqlParameter[] parameter =
            {
                new SqlParameter("id", id),
            };
            return _Repository.ExecuteProcedure("[dbo].[prGET_CUSTUMERS]");
        }
    }


}
