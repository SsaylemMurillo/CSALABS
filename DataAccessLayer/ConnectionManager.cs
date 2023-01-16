using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ConnectionManager
    {
        public DbConnection Connection;
        public ConnectionManager(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void OpenDataBase()
        {
            Connection.Open();
        }

        public void CloseDataBase()
        {
            Connection.Close();
        }
    }
}
