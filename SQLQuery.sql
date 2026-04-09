					CREATE DATABASE HotelManagementDB;
GO

USE HotelManagementDB;

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Username VARCHAR(50) UNIQUE,
    Gender VARCHAR(10),
    Password VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Address VARCHAR(255),
    Age INT,
    Languages VARCHAR(100),
    Country VARCHAR(50)
);
CREATE TABLE Hotels (
    HotelId INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50),
    RoomType VARCHAR(20),
    Amenities VARCHAR(100)
);

CREATE PROCEDURE sp_RegisterUser
(
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Username VARCHAR(50),
    @Gender VARCHAR(10),
    @Password VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address VARCHAR(255),
    @Age INT,
    @Languages VARCHAR(100),
    @Country VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Users
    VALUES (@FirstName,@LastName,@Username,@Gender,@Password,@Email,@Phone,@Address,@Age,@Languages,@Country)
END

CREATE PROCEDURE sp_LoginUser
(
    @Username VARCHAR(50),
    @Password VARCHAR(100)
)
AS
BEGIN
    SELECT * FROM Users
    WHERE Username = @Username AND Password = @Password
END

CREATE PROCEDURE sp_GetUsers
AS
BEGIN
    SELECT * FROM Users
END

CREATE PROCEDURE sp_RegisterHotel
(
    @Username VARCHAR(50),
    @RoomType VARCHAR(20),
    @Amenities VARCHAR(100)
)
AS
BEGIN
    INSERT INTO Hotels VALUES(@Username,@RoomType,@Amenities)
END

CREATE PROCEDURE sp_GetHotels
AS
BEGIN
    SELECT * FROM Hotels
END



ALTER TABLE Hotels
ADD CONSTRAINT FK_UserHotel
FOREIGN KEY (Username) REFERENCES Users(Username);


ALTER TABLE Users ADD Role VARCHAR(20) DEFAULT 'User';



ALTER PROCEDURE sp_LoginUser
(
    @Username VARCHAR(50),
    @Password VARCHAR(100)
)
AS
BEGIN
    SELECT Username, Role 
    FROM Users
    WHERE Username = @Username AND Password = @Password
END


select * from  Users