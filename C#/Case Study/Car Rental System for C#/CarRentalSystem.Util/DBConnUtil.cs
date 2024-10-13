using System;
using System.Data.SqlClient;
using System.Configuration;

namespace CarRentalSystem.Utils
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection(string dbName)
        {
            // Read connection string from a configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["CarRentalSystem"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
