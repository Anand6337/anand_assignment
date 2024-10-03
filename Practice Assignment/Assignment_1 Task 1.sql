--Assignment : 1 ( Task 1 )--

--Create the Database--
CREATE DATABASE TechShop;
USE TechShop;

--Define Schema for the Tables--
--Customers Table--
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(15) NOT NULL UNIQUE,
    Address VARCHAR(255)
);

--Products Table--
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName VARCHAR(100) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL
);

--Orders Table--
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    OrderDate DATETIME NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

--OrderDetails Table--
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

--Inventory Table--
CREATE TABLE Inventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT,
    QuantityInStock INT NOT NULL,
    LastStockUpdate DATETIME NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

--ER Diagram attached with the file--

--Primary and Foreign Key Constraints have been included in above schema--

--Inserting Sample Records--
INSERT INTO Customers (FirstName, LastName, Email, Phone, Address)
VALUES 
('John', 'Doe', 'john.doe@email.com', '1234567890', '123 Main St'),
('Jane', 'Smith', 'jane.smith@email.com', '0987654321', '456 Oak St'),
('Michael', 'Johnson', 'michael.johnson@email.com', '5555555555', '789 Pine St'),
('Emily', 'Davis', 'emily.davis@email.com', '1112223333', '321 Elm St'),
('David', 'Brown', 'david.brown@email.com', '4446667777', '654 Maple St'),
('Sarah', 'Wilson', 'sarah.wilson@email.com', '7778889999', '987 Cedar St'),
('James', 'Moore', 'james.moore@email.com', '3334445555', '321 Spruce St'),
('Laura', 'Taylor', 'laura.taylor@email.com', '2223334444', '987 Birch St'),
('Robert', 'Anderson', 'robert.anderson@email.com', '1115556666', '654 Walnut St'),
('Jessica', 'Martinez', 'jessica.martinez@email.com', '9998887777', '456 Cherry St');

INSERT INTO Products (ProductName, Description, Price)
VALUES 
('Laptop', 'High-performance laptop', 1200.99),
('Smartphone', 'Latest smartphone model', 899.99),
('Tablet', '10-inch display tablet', 499.99),
('Headphones', 'Wireless noise-cancelling headphones', 199.99),
('Smartwatch', 'Fitness tracker with smartwatch features', 299.99),
('Monitor', '27-inch 4K monitor', 349.99),
('Keyboard', 'Mechanical keyboard with RGB lighting', 89.99),
('Mouse', 'Wireless ergonomic mouse', 49.99),
('Printer', 'Wireless all-in-one printer', 159.99),
('Router', 'Dual-band wireless router', 129.99);

INSERT INTO Orders (CustomerID, OrderDate, TotalAmount)
VALUES 
(1, '2024-09-01', 1300.98),
(2, '2024-09-03', 999.99),
(3, '2024-09-05', 699.99),
(4, '2024-09-06', 249.99),
(5, '2024-09-07', 1499.98),
(6, '2024-09-08', 439.98),
(7, '2024-09-09', 799.99),
(8, '2024-09-10', 119.98),
(9, '2024-09-11', 509.99),
(10, '2024-09-12', 269.99);

INSERT INTO OrderDetails (OrderID, ProductID, Quantity)
VALUES 
(1, 1, 1),
(1, 2, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1),
(5, 6, 1),
(6, 7, 1),
(6, 8, 1),
(7, 1, 1);

INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate)
VALUES 
(1, 50, '2024-08-01'),
(2, 30, '2024-08-01'),
(3, 40, '2024-08-01'),
(4, 25, '2024-08-01'),
(5, 35, '2024-08-01'),
(6, 20, '2024-08-01'),
(7, 45, '2024-08-01'),
(8, 60, '2024-08-01'),
(9, 15, '2024-08-01'),
(10, 10, '2024-08-01');

--Displaying Table--
SELECT * FROM Customers
SELECT * FROM Inventory
SELECT * FROM OrderDetails
SELECT * FROM Orders
SELECT * FROM Products
