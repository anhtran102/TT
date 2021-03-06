
ALTER PROCEDURE [cmn].[UpdateInputCash]
	@Cash money,
	@FromAccount VARCHAR(20),
	@ToAccount VARCHAR(20),
	@Payer NVARCHAR(100),
	@PayDate DateTime,
	@ID INT
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	UPDATE cmn.InputCash SET Cash = @Cash, FromAccount =@FromAccount, ToAccount = @ToAccount, Payer =@Payer, PayDate = @PayDate, LastUpdated = SYSDATETIME() WHERE Id = @ID
COMMIT TRAN
