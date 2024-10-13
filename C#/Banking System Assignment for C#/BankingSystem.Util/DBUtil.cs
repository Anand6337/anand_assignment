using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Util
{
    public static class DBUtil
    {
        public static SqlConnection GetDBConn()
        {
            string connectionString = "Server=localhost;Database=HMBank;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
