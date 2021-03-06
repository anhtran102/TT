
ALTER PROCEDURE [cmn].[UpdateAccount]
	@AccountNo NVARCHAR(20),
	@AccountName NVARCHAR(100),
	@BankName NVARCHAR(100),
	@Phone VARCHAR(15),
	@IsOwn BIT,
	@ID INT
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	UPDATE cmn.AccountBank SET AccountNo = @AccountNo, AccountName =@AccountName, BankName = @BankName, Phone =@Phone, IsOwn = @IsOwn, LastUpdated = SYSDATETIME() WHERE Id = @ID
COMMIT TRAN
