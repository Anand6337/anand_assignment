using CarRentalSystem.Entity;
using CarRentalSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarRentalSystem.BusinessLayer
{
    public class CarRepository : ICarLeaseRepository
    {
        // Method to add a new vehicle to the system
        public void AddCar(Car car)
        {
            SqlConnection connection = null;
            try
            {
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                string query = "INSERT INTO Vehicle (make, model, year, dailyRate, status, passengerCapacity, engineCapacity) " +
                               "VALUES (@Make, @Model, @Year, @DailyRate, @Status, @PassengerCapacity, @EngineCapacity)";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to avoid SQL Injection
                command.Parameters.AddWithValue("@Make", car.Make);
                command.Parameters.AddWithValue("@Model", car.Model);
                command.Parameters.AddWithValue("@Year", car.Year);
                command.Parameters.AddWithValue("@DailyRate", car.DailyRate);
                command.Parameters.AddWithValue("@Status", car.Status);
                command.Parameters.AddWithValue("@PassengerCapacity", car.PassengerCapacity);
                command.Parameters.AddWithValue("@EngineCapacity", car.EngineCapacity);

                command.ExecuteNonQuery();
                Console.WriteLine("Vehicle added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding the vehicle: " + ex.Message);
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

        public void AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateTotalRevenue()
        {
            throw new NotImplementedException();
        }

        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Car FindCarById(int carID)
        {
            throw new NotImplementedException();
        }

        public Customer FindCustomerById(int customerID)
        {
            throw new NotImplementedException();
        }

        public Lease GetLeaseById(int leaseId)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetPaymentHistoryByCustomer(int customerID)
        {
            throw new NotImplementedException();
        }

        public List<Lease> ListActiveLeases()
        {
            throw new NotImplementedException();
        }

        // Method to list all available vehicles
        public List<Car> ListAvailableCars()
        {
            SqlConnection connection = null;
            List<Car> cars = new List<Car>();
            try
            {
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                connection.Open();

                string query = "SELECT * FROM Vehicle WHERE status = 'available'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Car car = new Car
                    {
                        CarID = Convert.ToInt32(reader["vehicleID"]),
                        Make = reader["make"].ToString(),
                        Model = reader["model"].ToString(),
                        Year = Convert.ToInt32(reader["year"]),
                        DailyRate = (decimal)Convert.ToDouble(reader["dailyRate"]),
                        Status = reader["status"].ToString(),
                        PassengerCapacity = Convert.ToInt32(reader["passengerCapacity"]),
                        EngineCapacity = (int)Convert.ToDouble(reader["engineCapacity"])
                    };
                    cars.Add(car);
                }

                Console.WriteLine("Available vehicles listed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while listing available vehicles: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
            return cars;
        }

        public List<Customer> ListCustomers()
        {
            throw new NotImplementedException();
        }

        public List<Lease> ListLeaseHistory()
        {
            throw new NotImplementedException();
        }

        public List<Car> ListRentedCars()
        {
            throw new NotImplementedException();
        }

        public void RecordPayment(Lease lease, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void RemoveCar(int carID)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(int customerID)
        {
            throw new NotImplementedException();
        }

        public Lease ReturnCar(int leaseId)
        {
            throw new NotImplementedException();
        }

        // (You can implement similar methods for updating and removing vehicles)
    }
}
