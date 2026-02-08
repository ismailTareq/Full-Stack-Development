-- 1. Hotels
CREATE TABLE Hotels (
    HotelID INT PRIMARY KEY,
    Name VARCHAR(100),
    Address VARCHAR(255),
    City VARCHAR(100),
    StarRating INT,
    ContactNumber VARCHAR(50)
);

-- 2. Staff
CREATE TABLE Staff (
    StaffID INT PRIMARY KEY,
    FullName VARCHAR(100),
    Position VARCHAR(50),
    Salary DECIMAL(10,2),
    HotelID INT,
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
);

-- 3. Rooms
CREATE TABLE Rooms (
    RoomNumber INT,
    HotelID INT,
    RoomType VARCHAR(50),
    Capacity INT,
    DailyRate DECIMAL(10,2),
    Availability BOOLEAN DEFAULT TRUE,
    PRIMARY KEY (RoomNumber, HotelID),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
);

-- 4. Amenities
CREATE TABLE Amenities (
    RoomNumber INT,
    HotelID INT,
    Amenity VARCHAR(100),
    PRIMARY KEY (RoomNumber, HotelID, Amenity),
    FOREIGN KEY (RoomNumber, HotelID) REFERENCES Rooms(RoomNumber, HotelID)
);

-- 5. Services
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY,
    ServiceName VARCHAR(100),
    Charge DECIMAL(10,2),
    HotelID INT,
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
);

-- 6. Guests
CREATE TABLE Guests (
    GuestID INT PRIMARY KEY,
    FullName VARCHAR(100),
    Nationality VARCHAR(50),
    PassportNumber VARCHAR(50),
    DateOfBirth DATE
);

-- 7. Guest Contact Details
CREATE TABLE Guest_Contact_Details (
    GuestID INT,
    Detail VARCHAR(255),
    PRIMARY KEY (GuestID, Detail),
    FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)
);

-- 8. Reservations
CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY,
    BookingDate DATE,
    CheckInDate DATE,
    CheckOutDate DATE,
    ReservationStatus VARCHAR(50),
    TotalPrice DECIMAL(10,2),
    ConfirmationNumber VARCHAR(100)
);

-- 9. Reservations_Rooms 
CREATE TABLE Reservations_Rooms (
    ReservationID INT,
    RoomNumber INT,
    HotelID INT,
    PRIMARY KEY (ReservationID, RoomNumber, HotelID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (RoomNumber, HotelID) REFERENCES Rooms(RoomNumber, HotelID)
);

-- 10. Reservations_Guests 
CREATE TABLE Reservations_Guests (
    ReservationID INT,
    GuestID INT,
    NumberOfAdults INT,
    NumberOfChildren INT,
    PRIMARY KEY (ReservationID, GuestID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)
);

-- 11. Payments
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY,
    Method VARCHAR(50),
    Date DATE,
    Amount DECIMAL(10,2),
    ConfirmationNumber VARCHAR(100)
);

-- 12. Reservations_Payments
CREATE TABLE Reservations_Payments (
    ReservationID INT,
    PaymentID INT,
    PRIMARY KEY (ReservationID, PaymentID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID)
);

-- 13. ReservationService 
CREATE TABLE ReservationService (
    ServiceID INT,
    ReservationID INT,
    RequestDate DATE,
    StaffID INT,
    PRIMARY KEY (ServiceID, ReservationID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
);