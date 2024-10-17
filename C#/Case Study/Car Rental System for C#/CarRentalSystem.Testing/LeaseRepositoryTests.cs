using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.BusinessLayer.Repository;
using NUnit.Framework;
using CarRentalSystem.Entity;
using CarRentalSystem.BusinessLayer;

namespace CarRentalSystem.Testing
{
    [TestFixture]
    public class LeaseRepositoryTests
    {
        private LeaseRepository _leaseRepo;

        [SetUp]
        public void Setup()
        {
            // Initialize the lease repository
            _leaseRepo = new LeaseRepository();
        }

        [Test]
        public void AddLease_ShouldCreateLeaseSuccessfully()
        {
            // Arrange
            var lease = new Lease(vehicleID: 1, customerID: 2, startDate: DateTime.Now, endDate: DateTime.Now.AddDays(7), type: "");

            // Act
            _leaseRepo.CreateLease(lease);
            var createdLease = _leaseRepo.ListActiveLeases();


            // Assert
            Assert.IsNotNull(createdLease, "Lease should be created successfully.");
            Assert.AreEqual(lease.VehicleID, createdLease.vehicleID, "Car ID in the lease should match.");
        }

        [Test]
        public void GetLeaseById_ShouldRetrieveLeaseSuccessfully()
        {
            // Arrange
            int leaseId = 1; // Assuming the lease ID exists in the database
            var expectedLease = new Lease(leaseId, 1, 2, DateTime.Now, DateTime.Now.AddDays(7), "Lease Type");

            // Act
            var lease = _leaseRepo.ListActiveLeases();

            // Assert
            Assert.IsNotNull(lease, "Lease should be retrieved successfully.");
            Assert.AreEqual(expectedLease.LeaseID, lease.LeaseID, "Lease ID should match.");
        }


        [Test]
        public void AddLease_ShouldThrowException_WhenCustomerOrCarNotFound()
        {
            // Arrange
            int nonExistentCustomerId = 999; // Customer that does not exist
            int validCarId = 2;
            var lease = new Lease(nonExistentCustomerId, validCarId, DateTime.Now, DateTime.Now.AddDays(7), 3500);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _leaseRepo.AddLease(lease));
            Assert.That(ex.Message, Is.EqualTo("Customer ID not found."));
        }

        [Test]
        public void GetLeaseById_ShouldThrowException_WhenLeaseNotFound()
        {
            // Arrange
            int nonExistentLeaseId = 999; // Lease that does not exist

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _leaseRepo.ListActiveLeases());
            Assert.That(ex.Message, Is.EqualTo("Lease ID not found."));
        }

    }

}
