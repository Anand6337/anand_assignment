using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using CarRentalSystem.BusinessLayer;
using CarRentalSystem.Entity;
using CarRentalSystem.Utils;

namespace CarRentalSystem.BusinessLayer.Repository
{
    public class CustomerRepository
    {
        private string connectionString;

        public CustomerRepository()
        {
            // Get the connection string from the configuration file
            connectionString = ConfigurationManager.ConnectionStrings["CarRentalSystem"].ConnectionString;
        }

        // Add a new customer
        public void AddCustomer(Customer customer)
        {
            SqlConnection connection = null;
            try
            {
                // Initialize and open the database connection
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                // Prepare the SQL query
                string query = "INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber) VALUES (@FirstName, @LastName, @Email, @Phone)";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to avoid SQL injection
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Phone", customer.PhoneNumber);

                // Execute the query
                command.ExecuteNonQuery();
                Console.WriteLine("Customer added successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed properly
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
        }



        // Update existing customer information
        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Customers SET firstName = @firstName, lastName = @lastName, email = @email, phoneNumber = @phoneNumber WHERE customerID = @customerID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@customerID", customer.CustomerID);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // Retrieve all customers
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = DBConnUtil.GetConnection("CarRentalSystem"))
            {
                // Correcting the table name to 'Customer'
                string query = "SELECT customerID, firstName, lastName, email, phoneNumber FROM Customer";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4)
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

    }
}
