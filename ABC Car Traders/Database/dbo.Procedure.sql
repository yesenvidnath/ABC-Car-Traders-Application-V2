CREATE PROCEDURE RegisterUser
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @Role NVARCHAR(20),
    @ProfileImage VARBINARY(MAX),
    @Address NVARCHAR(255),
    @UserID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the username already exists
    IF EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
    BEGIN
        RAISERROR('Username already exists', 16, 1);
        RETURN;
    END

    -- Insert the new user
    INSERT INTO Users (FirstName, LastName, Email, Phone, Username, Password, Role, ProfileImage, Address)
    OUTPUT INSERTED.UserID INTO @UserID
    VALUES (@FirstName, @LastName, @Email, @Phone, @Username, @Password, @Role, @ProfileImage, @Address);

    -- Insert into the relevant table based on the role
    IF @Role = 'Customer'
    BEGIN
        INSERT INTO Customers (UserID) VALUES (@UserID);
    END
    ELSE IF @Role = 'Admin'
    BEGIN
        INSERT INTO Admin (UserID) VALUES (@UserID);
    END
END
