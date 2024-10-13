using CarRentalSystem.Entity;
using CarRentalSystem.BusinessLayer;
using CarRentalSystem.Utils;
using CarRentalSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarRentalSystem.BusinessLayer.Repository;

namespace CarRentalSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize repository and database connection
            ICarLeaseRepository carLeaseRepo = new ICarLeaseRepositoryImpl();
            CustomerRepository customerRepo = new CustomerRepository(); // New customer repository
            SqlConnection connection = null;

            try
            {
                // Establish a connection using the DBConnUtil class
                connection = DBConnUtil.GetConnection("CarRentalSystem");
                Console.WriteLine("Database connection successful!");

                bool exit = false;

                // Main loop for the menu-driven application
                while (!exit)
                {
                    Console.WriteLine("\n--- Car Rental System Menu ---");
                    Console.WriteLine("1. Add New Customer");
                    Console.WriteLine("2. Update Customer");
                    Console.WriteLine("3. List All Customers");
                    Console.WriteLine("4. Add New Car");
                    Console.WriteLine("5. Create Lease");
                    Console.WriteLine("6. Return Car");
                    Console.WriteLine("7. List Available Cars");
                    Console.WriteLine("8. List Active Leases");
                    Console.WriteLine("9. Record Payment");
                    Console.WriteLine("10. Exit");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddNewCustomer(customerRepo);
                            break;
                        case "2":
                            UpdateCustomer(customerRepo);
                            break;
                        case "3":
                            ListAllCustomers(customerRepo);
                            break;
                        case "4":
                            AddNewVehicle(carLeaseRepo);
                            break;
                        case "5":
                            CreateNewLease(carLeaseRepo);
                            break;
                        case "6":
                            ReturnCar(carLeaseRepo);
                            break;
                        case "7":
                            ListAvailableCars(carLeaseRepo);
                            break;
                        case "8":
                            ShowActiveLeases(carLeaseRepo); // Renamed method
                            break;
                        case "9":
                            RecordPayment(carLeaseRepo);
                            break;
                        case "10":
                            exit = true;
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice! Please select a valid option.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection when done
                if (connection != null)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
        }

        // Method to add a new customer
        static void AddNewCustomer(CustomerRepository customerRepo)
        {
            try
            {
                Console.WriteLine("Enter Customer First Name: ");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter Customer Last Name: ");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter Customer Email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Enter Customer Phone Number: ");
                string phone = Console.ReadLine();

                Customer newCustomer = new Customer(firstName, lastName, email, phone);
                customerRepo.AddCustomer(newCustomer);

                Console.WriteLine("Customer added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Method to update customer information
        static void UpdateCustomer(CustomerRepository customerRepo)
        {
            try
            {
                Console.WriteLine("Enter Customer ID to update: ");
                int customerId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Customer First Name: ");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter Customer Last Name: ");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter Customer Email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Enter Customer Phone Number: ");
                string phone = Console.ReadLine();

                Customer updatedCustomer = new Customer
                {
                    CustomerID = customerId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone
                };

                customerRepo.UpdateCustomer(updatedCustomer);

                Console.WriteLine("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Method to list all customers
        static void ListAllCustomers(CustomerRepository customerRepo)
        {
            try
            {
                List<Customer> customers = customerRepo.GetAllCustomers();
                Console.WriteLine("\n--- List of Customers ---");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Method to add a new car
        static void AddNewVehicle(ICarLeaseRepository carLeaseRepo)
        {
            Console.WriteLine("Enter Vehicle Make: ");
            string make = Console.ReadLine();

            Console.WriteLine("Enter Vehicle Model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Enter Vehicle Year: ");
            int year = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Vehicle Daily Rate: ");
            double dailyRate = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter Passenger Capacity: ");
            int passengerCapacity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Engine Capacity: ");
            double engineCapacity = Convert.ToDouble(Console.ReadLine());

            Car newVehicle = new Car(make, model, year, dailyRate, "available", passengerCapacity, engineCapacity);
            carLeaseRepo.AddCar(newVehicle);
        }

        // List available cars
        static void ListAvailableCars(ICarLeaseRepository carLeaseRepo)
        {
            List<Car> availableCars = carLeaseRepo.ListAvailableCars();
            Console.WriteLine("\n--- Available Cars ---");
            foreach (Car car in availableCars)
            {
                Console.WriteLine($"ID: {car.CarID}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Daily Rate: {car.DailyRate}");
            }
        }

        // Method to create a lease
// Method to create a lease
static void CreateNewLease(ICarLeaseRepository leaseRepo)
{
    try
    {
        Console.WriteLine("Enter Vehicle ID: ");
        int vehicleID = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter Customer ID: ");
        int customerID = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter Lease Start Date (yyyy-mm-dd): ");
        DateTime startDate = Convert.ToDateTime(Console.ReadLine());

        Console.WriteLine("Enter Lease End Date (yyyy-mm-dd): ");
        DateTime endDate = Convert.ToDateTime(Console.ReadLine());

        Console.WriteLine("Enter Lease Type (DailyLease or MonthlyLease): ");
        string type = Console.ReadLine();

        // Call the CreateLease method with all required parameters
        leaseRepo.CreateLease(vehicleID, customerID, startDate, endDate, type);
        
        Console.WriteLine("Lease created successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}


        // Method to return a car
        static void ReturnCar(ICarLeaseRepository carLeaseRepo)
        {
            try
            {
                Console.WriteLine("Enter Lease ID: ");
                int leaseId = Convert.ToInt32(Console.ReadLine());

                carLeaseRepo.ReturnCar(leaseId);
                Console.WriteLine("Car returned successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // List active leases
        static void ShowActiveLeases(ICarLeaseRepository leaseRepo)
        {
            List<Lease> activeLeases = leaseRepo.ListofActiveLeases();
            Console.WriteLine("\n--- Active Leases ---");
            foreach (var lease in activeLeases)
            {
                Console.WriteLine($"Lease ID: {lease.LeaseID}, Car ID: {lease.VehicleID}, Customer ID: {lease.CustomerID}, Start Date: {lease.StartDate}, End Date: {lease.EndDate}, Lease Type: {lease.Type}");
            }
        }

        // Method to record a payment
        static void RecordPayment(ICarLeaseRepository leaseRepo)
        {
            Console.WriteLine("Enter Lease ID: ");
            int leaseId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Payment Amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            leaseRepo.RecordPayment(leaseId, amount);
        }
    }
}
