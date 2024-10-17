using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarRentalSystem.BusinessLayer;
using CarRentalSystem.Entity;

namespace CarRentalSystem.Testing
{
    [TestFixture]
    public class CarRepositoryTests
    {
        private CarRepository _carRepo;

        [SetUp]
        public void Setup()
        {
            // Initialize the car repository (mock database setup can be used here)
            _carRepo = new CarRepository();
        }

        [Test]
        public void AddCar_ShouldCreateCarSuccessfully()
        {
            // Arrange
            var car = new Car("Toyota", "Camry", 2020, 500m, "available", 5, 2500);

            // Act
            _carRepo.AddCar(car);
            var createdCar = _carRepo.FindCarById(car.VehicleID); // Assuming GetCarById method exists

            // Assert
            Assert.IsNotNull(createdCar, "Car should be created successfully.");
            Assert.AreEqual(car.Make, createdCar.Make, "Car make should match.");
 
        }
    }

}
