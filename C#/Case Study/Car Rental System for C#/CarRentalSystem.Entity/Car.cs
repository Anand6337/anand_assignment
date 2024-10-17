using System;

namespace CarRentalSystem.Entity
{
    public class Car
    {
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public string Status { get; set; } // "available" or "notAvailable"
        public int PassengerCapacity { get; set; }
        public int EngineCapacity { get; set; } // in CC

        // Default constructor
        public Car() { }

        // Constructor with all properties
        public Car(int vehicleID, string make, string model, int year, decimal dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }

        // Constructor without VehicleID (for new cars where ID is generated later)
        public Car(string make, string model, int year, decimal dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }
    }
}
