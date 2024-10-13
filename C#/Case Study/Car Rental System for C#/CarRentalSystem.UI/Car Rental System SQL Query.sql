

use CarRentalSystem;

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

INSERT INTO Vehicle (make, model, year, dailyRate, status, passengerCapacity, engineCapacity)
VALUES 
('Toyota', 'Corolla', 2020, 50.00, 'available', 5, 1.8),
('Honda', 'Civic', 2019, 55.00, 'available', 5, 2.0),
('Ford', 'Mustang', 2021, 75.00, 'notAvailable', 4, 2.3),
('Tesla', 'Model S', 2022, 100.00, 'available', 5, 0.0),
('Chevrolet', 'Malibu', 2020, 45.00, 'available', 5, 1.5),
('BMW', 'X5', 2021, 90.00, 'notAvailable', 5, 3.0),
('Audi', 'A4', 2018, 70.00, 'available', 5, 2.0),
('Mercedes', 'C-Class', 2020, 80.00, 'available', 5, 2.5),
('Hyundai', 'Elantra', 2019, 50.00, 'available', 5, 1.6),
('Kia', 'Sorento', 2021, 65.00, 'available', 7, 2.4);

INSERT INTO Customer (firstName, lastName, email, phoneNumber)
VALUES 
('John', 'Doe', 'john.doe@example.com', '1234567890'),
('Jane', 'Smith', 'jane.smith@example.com', '0987654321'),
('Mike', 'Johnson', 'mike.johnson@example.com', '5551234567'),
('Emily', 'Williams', 'emily.williams@example.com', '6669876543'),
('Chris', 'Brown', 'chris.brown@example.com', '7776543210'),
('David', 'Wilson', 'david.wilson@example.com', '8883456789'),
('Laura', 'Taylor', 'laura.taylor@example.com', '9992345678'),
('Sarah', 'Anderson', 'sarah.anderson@example.com', '1111234567'),
('Tom', 'Clark', 'tom.clark@example.com', '2229876543'),
('Linda', 'Moore', 'linda.moore@example.com', '3335678901');

INSERT INTO Lease (vehicleID, customerID, startDate, endDate, type)
VALUES 
(1, 1, '2023-09-01', '2023-09-07', 'DailyLease'),
(2, 2, '2023-08-20', '2023-08-27', 'DailyLease'),
(3, 3, '2023-09-01', '2023-09-30', 'MonthlyLease'),
(4, 4, '2023-08-15', '2023-09-15', 'MonthlyLease'),
(5, 5, '2023-09-05', '2023-09-12', 'DailyLease'),
(6, 6, '2023-09-10', '2023-09-17', 'DailyLease'),
(7, 7, '2023-08-01', '2023-08-31', 'MonthlyLease'),
(8, 8, '2023-09-05', '2023-09-12', 'DailyLease'),
(9, 9, '2023-07-01', '2023-07-31', 'MonthlyLease'),
(10, 10, '2023-09-15', '2023-09-22', 'DailyLease');

INSERT INTO Payment (leaseID, paymentDate, amount)
VALUES 
(1, '2023-09-01', 350.00),
(2, '2023-08-20', 385.00),
(3, '2023-09-01', 1500.00),
(4, '2023-08-15', 3000.00),
(5, '2023-09-05', 315.00),
(6, '2023-09-10', 385.00),
(7, '2023-08-01', 1400.00),
(8, '2023-09-05', 455.00),
(9, '2023-07-01', 2000.00),
(10, '2023-09-15', 455.00);

select * from Vehicle;
select * from Customer;
select * from Lease;
select * from Payment;