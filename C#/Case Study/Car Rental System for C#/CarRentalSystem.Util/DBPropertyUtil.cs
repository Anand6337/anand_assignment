using System;
using System.Configuration;

namespace CarRentalSystem.Utils
{
    public static class DBPropertyUtil
    {
        // Static method to fetch the connection string from the configuration file
        public static string GetConnectionString(string propertyName)
        {
            try
            {
                // Retrieves the connection string from the app.config or web.config
                string connectionString = ConfigurationManager.ConnectionStrings[propertyName]?.ConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException("Connection string not found for the property: " + propertyName);
                }

                return connectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading the connection string: " + ex.Message);
            }
        }
    }
}
