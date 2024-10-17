using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarRentalSystem.Utils;
using NUnit.Framework;

namespace CarRentalSystem.BusinessLayer.Repository
{
    [TestFixture]
    public class LeaseRepository
    {
        [Test]
        public void CreateLease(int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
        {
            SqlConnection connection = null;
            try
            {
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                string query = "INSERT INTO Lease (vehicleID, customerID, startDate, endDate, type) " +
                               "VALUES (@vehicleID, @customerID, @startDate, @endDate, @type)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@vehicleID", vehicleID);
                command.Parameters.AddWithValue("@customerID", customerID);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.Parameters.AddWithValue("@type", type);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Lease created successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to create lease.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating lease: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
        }

        [Test]
        public List<Lease> ListActiveLeases()
        {
            SqlConnection connection = null;
            List<Lease> leases = new List<Lease>();
            try
            {
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                string query = "SELECT * FROM Lease WHERE endDate >= GETDATE()";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lease lease = new Lease(
                        Convert.ToInt32(reader["leaseID"]),
                        Convert.ToInt32(reader["vehicleID"]),
                        Convert.ToInt32(reader["customerID"]),
                        Convert.ToDateTime(reader["startDate"]),
                        Convert.ToDateTime(reader["endDate"]),
                        reader["type"].ToString()
                    );
                    leases.Add(lease);
                }

                Console.WriteLine("Active leases listed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while listing active leases: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
            return leases;
        }

        public void UpdateLease(int leaseID, DateTime newEndDate)
        {
            SqlConnection connection = null;
            try
            {
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                string query = "UPDATE Lease SET endDate = @newEndDate WHERE leaseID = @leaseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@newEndDate", newEndDate);
                command.Parameters.AddWithValue("@leaseID", leaseID);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Lease updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update lease.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating lease: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
        }

        public void EndLease(int leaseID)
        {
            SqlConnection connection = null;
            try
            {
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                string query = "DELETE FROM Lease WHERE leaseID = @leaseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@leaseID", leaseID);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Lease ended successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to end lease.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while ending lease: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
        }
    }
}
