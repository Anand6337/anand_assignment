using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CarRentalSystem.Entity;
using CarRentalSystem.Exceptions;
using CarRentalSystem.BusinessLayer; 
using CarRentalSystem.Utils;

namespace CarRentalSystem.BusinessLayer
{
    public class ICarLeaseRepositoryImpl : ICarLeaseRepository
    {
        // Mock database lists for cars, customers, leases, and payments
        private List<Car> cars = new List<Car>();
        private List<Customer> customers = new List<Customer>();
        private List<Lease> leases = new List<Lease>();
        private List<Payment> payments = new List<Payment>();

        // Car Management
        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        public void RemoveCar(int carID)
        {
            var car = FindCarById(carID);
            if (car != null)
            {
                cars.Remove(car);
            }
        }

        public List<Car> ListAvailableCars()
        {
            return cars.Where(c => c.Status == "available").ToList();
        }

        public List<Car> ListRentedCars()
        {
            return cars.Where(c => c.Status == "notAvailable").ToList();
        }

        public Car FindCarById(int carID)
        {
            var car = cars.FirstOrDefault(c => c.VehicleID == carID);
            if (car == null)
            {
                throw new CarNotFoundException($"Car with ID {carID} not found.");
            }
            return car;
        }

        // Customer Management
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public void RemoveCustomer(int customerID)
        {
            var customer = FindCustomerById(customerID);
            if (customer != null)
            {
                customers.Remove(customer);
            }
        }

        public List<Customer> ListCustomers()
        {
            return customers.ToList();
        }

        public Customer FindCustomerById(int customerID)
        {
            var customer = customers.FirstOrDefault(c => c.CustomerID == customerID);
            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer with ID {customerID} not found.");
            }
            return customer;
        }

        // Lease Management
        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate, string type)
        {
            var car = FindCarById(carID);
            var customer = FindCustomerById(customerID);

            Lease lease = new Lease(leases.Count + 1, carID, customerID, startDate, endDate, type);
            leases.Add(lease);

            // Set car status to notAvailable after lease
            car.Status = "notAvailable";

            return lease;
        }

        public Lease ReturnCar(int leaseId)
        {
            Lease lease = GetLeaseById(leaseId); // Get lease by ID
            // Logic to mark car as available in the database and update lease status
            using (var connection = DBConnUtil.GetConnection("CarRentalDB"))
            {
                SqlCommand command = new SqlCommand("UPDATE Lease SET endDate = @endDate WHERE leaseId = @leaseId", connection);
                command.Parameters.AddWithValue("@endDate", DateTime.Now);
                command.Parameters.AddWithValue("@leaseId", leaseId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Mark the corresponding vehicle as available
                    var car = FindCarById(lease.VehicleID);
                    if (car != null)
                    {
                        car.Status = "available"; // Mark car as available
                    }
                }
                else
                {
                    throw new LeaseNotFoundException("Lease ID not found.");
                }
            }
            return lease;
        }

        public List<Lease> ListActiveLeases()
        {
            return leases.Where(l => DateTime.Now < l.EndDate).ToList();
        }

        public List<Lease> ListLeaseHistory()
        {
            return leases.ToList();
        }

        // Payment Handling
        public void RecordPayment(Lease lease, decimal amount)
        {
            Payment payment = new Payment(payments.Count + 1, lease.LeaseID, DateTime.Now, amount);
            payments.Add(payment);
        }

        public List<Payment> GetPaymentHistoryByCustomer(int customerID)
        {
            return payments.Where(p => leases.Any(l => l.CustomerID == customerID && l.LeaseID == p.LeaseID)).ToList();
        }

        public decimal CalculateTotalRevenue()
        {
            return payments.Sum(p => p.Amount);
        }

        // New method to get lease by ID
        public Lease GetLeaseById(int leaseId)
        {
            var lease = leases.FirstOrDefault(l => l.LeaseID == leaseId);
            if (lease == null)
            {
                throw new LeaseNotFoundException($"Lease with ID {leaseId} not found.");
            }
            return lease;
        }

        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        Lease ICarLeaseRepository.ReturnCar(int leaseID)
        {
            Lease lease = GetLeaseById(leaseID);
            // Return car logic...
            return lease;
        }

        public List<Lease> ListofActiveLeases()
        {
            throw new NotImplementedException();
        }

        public void RecordPayment(int lease, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
