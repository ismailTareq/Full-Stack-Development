-- 1. Categories
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100),
    Description VARCHAR(255),
    MainCategory INT,
    FOREIGN KEY (MainCategory) REFERENCES Categories(CategoryID)
);

-- 2. Suppliers
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100),
    Country VARCHAR(50),
    Email VARCHAR(255),
    Address VARCHAR(255),
    ContactNumber VARCHAR(50)
);

-- 3. Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100),
    Description TEXT,
    UnitPrice DECIMAL(10,2),
    StockQuantity INT,
    AddedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- 4. Products_Suppliers 
CREATE TABLE Products_Suppliers (
    SupplierID INT,
    ProductID INT,
    PRIMARY KEY (SupplierID, ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- 5. StockTransactions
CREATE TABLE StockTransactions (
    TransactionID INT PRIMARY KEY AUTO_INCREMENT,
    TranDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    QuantityChange INT,
    Type VARCHAR(20),
    Reference VARCHAR(255),
    ProductID INT,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- 6. Reviews
CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY AUTO_INCREMENT,
    Rating TINYINT,
    Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Comment TEXT,
    ProductID INT,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- 7. Customers
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY AUTO_INCREMENT,
    FullName VARCHAR(100),
    PhoneNumber VARCHAR(50),
    Email VARCHAR(255),
    ShippingAddress VARCHAR(255),
    RegistrationDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 8. Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY AUTO_INCREMENT,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(50),
    TotalAmount DECIMAL(10,2),
    CustomerID INT,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- 9. OrderItems
CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY AUTO_INCREMENT,
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    OrderID INT,
    ProductID INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- 10. Payments
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY AUTO_INCREMENT,
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Amount DECIMAL(10,2),
    Method VARCHAR(50),
    Status VARCHAR(50)
);

-- 11. Orders_Payments 
CREATE TABLE Orders_Payments (
    OrderID INT,
    PaymentID INT,
    PRIMARY KEY (OrderID, PaymentID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID)
);

-- 12. Shipments
CREATE TABLE Shipments (
    ShipmentID INT PRIMARY KEY AUTO_INCREMENT,
    ShipmentDate DATETIME,
    DeliveryDate DATETIME,
    Status VARCHAR(50),
    CarrierName VARCHAR(100),
    TrackingNumber VARCHAR(100),
    OrderID INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);