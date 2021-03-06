
CREATE PROCEDURE [cmn].[AddInputCash]
	@Cash money,
	@FromAccount VARCHAR(20),
	@ToAccount VARCHAR(20),
	@Payer NVARCHAR(100),
	@PayDate DateTime
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	INSERT INTO cmn.InputCash(Cash, FromAccount, ToAccount, Payer, PayDate) VALUES(@Cash, @FromAccount, @ToAccount, @Payer, @PayDate)
COMMIT TRAN
