using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Entity
{
    public class Car
    {
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public string Status { get; set; } // available, notAvailable
        public int PassengerCapacity { get; set; }
        public int EngineCapacity { get; set; }
        public double DailyRate1 { get; }
        public string V { get; }
        public double EngineCapacity1 { get; }
        public object CarID { get; set; }

        // Constructors
        public Car() { }

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

        public Car(string make, string model, int year, double dailyRate, string v, int passengerCapacity, double engineCapacity)
        {
            Make = make;
            Model = model;
            Year = year;
            DailyRate1 = dailyRate;
            V = v;
            PassengerCapacity = passengerCapacity;
            EngineCapacity1 = engineCapacity;
        }
    }
}

