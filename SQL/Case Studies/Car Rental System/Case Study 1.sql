--Case Study
--Group Eagle Wings
--Submitted By: Anand Salokiya and Daksh Yadav

CREATE DATABASE CarRentalSystem;

CREATE TABLE Vehicle (
    vehicleID INT PRIMARY KEY IDENTITY(1,1),
    make VARCHAR(50),
    model VARCHAR(50),
    year INT,
    dailyRate DECIMAL(10, 2),
    status VARCHAR(20) CHECK (status IN ('available', 'notAvailable')),
    passengerCapacity INT,
    engineCapacity DECIMAL(5, 2)
);

CREATE TABLE Customer (
    customerID INT PRIMARY KEY IDENTITY(1,1),
    firstName VARCHAR(50),
    lastName VARCHAR(50),
    email VARCHAR(100),
    phoneNumber VARCHAR(15)
);

CREATE TABLE Lease (
    leaseID INT PRIMARY KEY IDENTITY(1,1),
    vehicleID INT,
    customerID INT,
    startDate DATE,
    endDate DATE,
    type VARCHAR(20) CHECK (type IN ('DailyLease', 'MonthlyLease')),
    FOREIGN KEY (vehicleID) REFERENCES Vehicle(vehicleID),
    FOREIGN KEY (customerID) REFERENCES Customer(customerID)
);

CREATE TABLE Payment (
    paymentID INT PRIMARY KEY IDENTITY(1,1),
    leaseID INT,
    paymentDate DATE,
    amount DECIMAL(10, 2),
    FOREIGN KEY (leaseID) REFERENCES Lease(leaseID)
);