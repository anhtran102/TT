
ALTER PROCEDURE [cmn].[UpdateCustomer]	
	@CustomerName NVARCHAR(100),
	@Address NVARCHAR(1000),
	@Phone VARCHAR(15),
	@ID INT	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	UPDATE cmn.Customer SET CustomerName = @CustomerName, Address =@Address, Phone =@Phone, LastUpdated = SYSDATETIME() WHERE Id = @ID
COMMIT TRAN
