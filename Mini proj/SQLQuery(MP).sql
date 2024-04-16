create database train_reservation
use train_reservation

CREATE TABLE Trains (
    trainid int primary key identity(10000, 1),
    TrainName VARCHAR(255),
    DepartureStation VARCHAR(255),
    ArrivalStation VARCHAR(255),
    FirstClassFare DECIMAL(10,2),
    SecondClassFare DECIMAL(10,2),
    SleeperClassFare DECIMAL(10,2),
	totalberths int,
	availableberths int,
    Status VARCHAR(50)
);

INSERT INTO Trains VALUES
( 'Venad Express', 'Trivandrum Central', 'Shoranur Junction', 10000, 5000, 2500,100,75, 'inactive'),
( 'Cheran Express', 'Coimbatore Junction', 'Dindigul Junction', 1800, 900, 558,25,3, 'active'),
( 'Chennai Express', 'Chennai Central', 'Mumbai Central', 1200, 600, 312,123,35, 'active'),
( 'Kerala Express', 'New Delhi', 'Trivandrum Central', 2500, 1250, 750,25,1, 'inactive'),
( 'Island Express', 'Kanyakumari', 'Bangalore City', 1800, 900, 450,13,10, 'active'),
( 'Tirupathi Express', 'Bangalore', 'Tirupathi', 1200, 600, 300,50,25, 'active'),
( 'Goa Express', 'Bangalore Junction', 'Goa Junction', 399, 199, 100,45,22, 'active'),
( 'Katpadi Express', 'Chennai', 'Vellore', 550, 275, 183,50,22, 'inactive');

CREATE table Users (
    UserId INT  identity,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
   
);

INSERT INTO Users
VALUES
    ( 'lavanya', 'lav@123'),
    ( 'Dileep', 'dil@123'),
    ( 'Neela', 'neelu@123'),
    ( 'Hema', 'hem@123'),
    ( 'Ramesh', 'ram@123');

CREATE TABLE Admins (
    AdminId INT PRIMARY KEY identity,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
);

INSERT INTO Admins
VALUES
    ( 'admin1', 'adminpassword1'),
    ('admin2', 'adminpassword2'),
    ('admin3', 'adminpassword3');


CREATE TABLE BookedTickets (
    BookingId INT identity(101,1),
    TrainId INT ,
	trainame varchar(30),
    UserId INT, 
    totalfare INT,
	class varchar(20),
    BookingDate DATETIME NOT NULL,
    NumberOfTickets INT NOT NULL,
    FOREIGN KEY (TrainId) REFERENCES Trains(TrainId),
);


INSERT INTO BookedTickets
VALUES
    (10001, 'Venad Express', 1, 10000, '2nd class', '2024-04-12 08:00:00', 2),
    (10002, 'Cheran Express', 2, 2232, 'sleeper class', '2024-04-23 08:00:00', 4),
    (10003, 'Chennai Express', 3, 3600, '1st class', '2024-04-15 08:00:00', 3),
    (10001, 'Kerala Express', 4, 10000, '2nd class', '2024-04-10 08:00:00', 2),
    (10002, 'Island Express', 5, 558, 'sleeper class', '2024-04-30 08:00:00', 1);

CREATE TABLE cancelticket (
    cancelid INT PRIMARY KEY IDENTITY,
    bookid INT,
    userid INT,
    trainno INT,
    refund_amt INT,
	no_of_tickets int,
    FOREIGN KEY (trainno) REFERENCES Trains(TrainId)
);


INSERT INTO cancelticket 
VALUES ( 101, 1, 10001, 450,1),
       ( 102, 2, 10003, 750,2),
       ( 103, 3, 10002, 850,2);

-------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE AddTrainBasedOnStatus
    @TrainName VARCHAR(100),
    @Departurestation VARCHAR(100),
    @arrivalstation VARCHAR(100),
    @firstclass VARCHAR(20),
	@secondclass VARCHAR(20),
	@sleeperclass VARCHAR(20),
    @totalberths INT,
    @availableberths INT,
    @TStatus VARCHAR(20)
AS
BEGIN
    IF @TStatus IN ('active', 'inactive')
    BEGIN
	select * from Trains
        INSERT INTO Trains(TrainName, departurestation, arrivalstation, firstclassfare,secondclassFare,sleeperclassfare, TotalBerths, AvailableBerths,Status)
        VALUES (@TrainName, @departurestation, @arrivalstation, @firstclass, @secondclass,@sleeperclass, @totalberths, @availableberths, @TStatus);
    END
    ELSE
    BEGIN
        THROW 51000, 'Cannot add train with status other than active or inactive', 1;
    END
END;
---------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE UpdateTrainStatus
    @TrainId INT,
    @TrainStatus VARCHAR(20)
AS
BEGIN
    IF @TrainStatus IN ('active', 'inactive')
    BEGIN
        UPDATE Trains
        SET status = @TrainStatus
        WHERE TrainId = @TrainId;
        PRINT 'Train status updated successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Invalid train status. Please specify either active or inactive.';
    END
END;
----------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE DeleteTrain
    @TrainId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Trains
        SET Status = 'inactive'
        WHERE TrainId = @TrainId;

        COMMIT TRANSACTION;
        PRINT 'Train set to inactive successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error setting train to inactive.';
    END CATCH;
END;

----------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE UserLoginProcedure
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserType VARCHAR(10);
    DECLARE @UserId INT;

    SELECT @UserId = UserId
    FROM Users
    WHERE Username = @Username AND Password = @Password;

    IF @@ROWCOUNT > 0
    BEGIN
        SET @UserType = 'User';
    END
    ELSE
    BEGIN
        SET @UserType = 'Invalid';
    END

    SELECT @UserType AS UserType, @UserId AS UserId;
END

----------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE Cancel
    @BookingId INT,
    @UserId INT
AS
BEGIN
    DECLARE @TrainId INT;
    DECLARE @NumberOfTickets INT;

    SELECT TOP 1 @TrainId = TrainId, @NumberOfTickets = NumberOfTickets
    FROM BookedTickets
    WHERE BookingId = @BookingId AND UserId = @UserId;

    IF @@ROWCOUNT = 0
    BEGIN
        PRINT 'BookingId and UserId do not match.';
        RETURN;
    END;

    DELETE FROM BookedTickets
    WHERE BookingId = @BookingId AND UserId = @UserId;

    UPDATE Trains
    SET AvailableBerths = AvailableBerths + @NumberOfTickets
    WHERE TrainId = @TrainId;

    INSERT INTO cancelticket --(BookingId, UserId, TrainId, Refund_Amt, no_of_tickets)
    VALUES (@BookingId, @UserId, @TrainId, (@NumberOfTickets * 100), @NumberOfTickets);

    PRINT 'Ticket canceled successfully.';
END;

----------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE BookTrainTickets
    @TrainId INT,
    @UserId INT,
    @TrainName VARCHAR(255),
    @BookingDate DATETIME,
    @NumberOfTickets INT,
    @Class VARCHAR(20),
    @BookingId INT OUTPUT -- Output parameter for BookingId
AS
BEGIN
    DECLARE @TrainExists INT;
    DECLARE @UserExists INT;
    DECLARE @UserLoggedInId INT = 1; -- Example value, replace with actual logged-in user ID

    SELECT @TrainExists = COUNT(*)
    FROM Trains
    WHERE TrainId = @TrainId AND TrainName = @TrainName;

    IF @TrainExists = 0
    BEGIN
        PRINT 'Train ID or Train Name not found.';
        RETURN;
    END;

    SELECT @UserExists = COUNT(*)
    FROM Users
    WHERE UserId = @UserId;

    IF @UserExists = 0
    BEGIN
        PRINT 'Invalid User ID.';
        RETURN;
    END;

    IF @UserId != @UserLoggedInId
    BEGIN
        PRINT 'You can only book tickets for your own user ID.';
        RETURN;
    END;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @TotalFare DECIMAL(10, 2);
        SELECT @TotalFare =
            CASE
                WHEN @Class = '1st class' THEN FirstClassFare
                WHEN @Class = '2nd class' THEN SecondClassFare
                WHEN @Class = 'sleeper class' THEN SleeperClassFare
                ELSE 0
            END
        FROM Trains
        WHERE TrainId = @TrainId;

        IF @TotalFare = 0
        BEGIN
            PRINT 'Invalid class type or TrainId.';
            RETURN;
        END;

        SET @TotalFare = @TotalFare * @NumberOfTickets;

        INSERT INTO BookedTickets 
        VALUES (@TrainId, @TrainName, @UserId, @TotalFare, @Class, @BookingDate, @NumberOfTickets);

        UPDATE Trains
        SET availableberths = availableberths - @NumberOfTickets
        WHERE TrainId = @TrainId;

        SELECT @BookingId = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
        PRINT 'Ticket booked successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error booking ticket.';
    END CATCH;
END;




-----------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE AdminLoginProcedure
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserType VARCHAR(10);

    IF EXISTS (SELECT 1 FROM Admins WHERE Username = @Username AND Password = @Password)
    BEGIN
        SET @UserType = 'Admin';
    END
    ELSE
    BEGIN
        SET @UserType = 'Invalid';
    END

    SELECT @UserType AS UserType;
END
---------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE ViewTrain
AS
BEGIN
    SELECT * FROM trains WHERE status = 'active';
END;

---------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE AddUser
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    INSERT INTO [Users] (Username, [Password])
    VALUES (@Username, @Password);
END;
---------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE deleteuser
    @Userid VARCHAR(50)
AS
BEGIN
    DELETE FROM [Users] WHERE UserId = @Userid;
END
---------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE PrintTicket
    @TrainId INT
AS
BEGIN
    SELECT TOP 1 *
    FROM BookedTickets
    WHERE TrainId = @TrainId
    ORDER BY BookingId DESC; 
END;

====================================================================================================================================================
select * from trains
select * from admins
select * from users
select * from bookedtickets
select * from cancelticket

----------------------------------------------------------------------------------------------------------------------------------------------------


