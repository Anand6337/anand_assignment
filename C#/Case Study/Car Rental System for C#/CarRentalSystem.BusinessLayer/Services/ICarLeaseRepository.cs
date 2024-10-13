using System;
using System.Collections.Generic;
using CarRentalSystem.BusinessLayer;  
using CarRentalSystem.Entity;

namespace CarRentalSystem.BusinessLayer
{
    public interface ICarLeaseRepository
    {
        // Car Management
        void AddCar(Car car);
        void RemoveCar(int carID);
        List<Car> ListAvailableCars();
        List<Car> ListRentedCars();
        Car FindCarById(int carID);

        // Customer Management
        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerID);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);

        // Lease Management
        Lease CreateLease(int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type);
        Lease ReturnCar(int leaseId);
      
        List<Lease> ListofActiveLeases();
        List<Lease> ListLeaseHistory();

        // Payment Handling
        void RecordPayment(int lease, decimal amount);
        List<Payment> GetPaymentHistoryByCustomer(int customerID);
        decimal CalculateTotalRevenue();
        Lease GetLeaseById(int leaseId);
        //void ReturnCar(int leaseID);
    }
}
