CREATE TABLE Customer(
UserId UNIQUEIDENTIFIER primary key default newid(),
Username varchar(30),
Email varchar(20),
FirstName varchar(20),
LastName varchar(20),
CreatedOn DateTime,
IsActive bit
)

create table Supplier(
SupplierId UNIQUEIDENTIFIER primary key default newid(),
SupplierName varchar(50),
CreatedOn DateTime,
IsActive bit
)

create table Product(
ProductId UNIQUEIDENTIFIER primary key default newid(),
ProductName varchar(50),
UnitPrice decimal(18,2),
SupplierId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Supplier(SupplierId),
CreatedOn DateTime,
IsActive bit,
)

create table [Order](
OrderId UNIQUEIDENTIFIER primary key default newid(),
ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Product(ProductId),
OrderStatus int,
OrderType int,
OrderBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Customer(UserId),
OrderedOn DateTime,
ShippedOn DateTime,
IsActive bit
)

CREATE PROCEDURE sp_GetCustomer
AS
BEGIN
    SELECT * FROM Customer WITH(NOLOCK) ORDER BY UserId ASC
END

-----------------------------------------------------------------
CREATE PROCEDURE sp_InsertUpdateCustomer
(
@UserId varchar(max),
@Username VARCHAR(30),
@Email VARCHAR(20),
@FirstName VARCHAR(20),
@LastName VARCHAR(20),
@ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
    SET @ReturnCode = 'DCE100'
    IF(@UserId <> '')
    BEGIN
        IF EXISTS (SELECT 1 FROM Customer WHERE Username = @Username AND UserId <> @UserId)
        BEGIN
            SET @ReturnCode = 'DCE101'
            RETURN
        END
        IF EXISTS (SELECT 1 FROM Customer WHERE Email = @Email AND UserId <> @UserId)
        BEGIN
            SET @ReturnCode = 'DCE102'
            RETURN
        END

        UPDATE Customer SET
        Username = @Username,
        Email = @Email,
        FirstName = @FirstName,
        LastName = @LastName,
        IsActive = 1
        WHERE UserId = @UserId

        SET @ReturnCode = 'DCE100'
    END
    ELSE
    BEGIN
        IF EXISTS (SELECT 1 FROM Customer WHERE Username = @Username)
        BEGIN
            SET @ReturnCode = 'DCE101'
            RETURN
        END
        IF EXISTS (SELECT 1 FROM Customer WHERE Email = @Email)
        BEGIN
            SET @ReturnCode = 'DCE102'
            RETURN
        END

        INSERT INTO Customer (UserId,Username,Email,FirstName,LastName,CreatedOn,IsActive)
        VALUES (default,@Username,@Email,@FirstName,@LastName,Getdate(),1)

        SET @ReturnCode = 'DCE100'
    END
END

CREATE PROCEDURE sp_DeleteCustomer
(
@UserId varchar(max),
@ReturnCode VARCHAR(20) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @ReturnCode = 'DCE200'
    IF NOT EXISTS (SELECT 1 FROM Customer WHERE UserId = @UserId)
    BEGIN
        SET @ReturnCode ='DCE201'
        RETURN
    END
    ELSE
    BEGIN
        DELETE FROM Customer WHERE UserId = @UserId
        SET @ReturnCode = 'DCE200'
        RETURN
    END
END


CREATE PROCEDURE sp_ActiveOrdersbyCustomer
AS
BEGIN
SELECT C.UserId,
(C.FirstName + ' ' + C.LastName) AS FullName,
O.OrderId,
P.ProductName,
O.OrderStatus,
O.OrderType,
O.OrderedOn,
O.ShippedOn,
S.SupplierName
FROM [Order] O INNER JOIN Customer C ON O.OrderBy = C.UserId
INNER JOIN Product P ON O.ProductId = P.ProductId
INNER JOIN Supplier S ON P.SupplierId = S.SupplierId
WHERE O.IsActive = 1
END
select * from Customer