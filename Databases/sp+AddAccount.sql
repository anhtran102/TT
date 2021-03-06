
ALTER PROCEDURE [cmn].[AddAccount]
	@AccountNo NVARCHAR(20),
	@AccountName NVARCHAR(100),
	@BankName NVARCHAR(100),
	@Phone VARCHAR(15),
	@IsOwn BIT
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	INSERT INTO cmn.AccountBank(AccountNo, AccountName, BankName, Phone, IsOwn) VALUES(@AccountNo, @AccountName, @BankName, @Phone, @IsOwn)
COMMIT TRAN
