
ALTER PROCEDURE [cmn].[GetAllAccounts]	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

SELECT Id, AccountNo, AccountName,BankName, Phone, IsOwn  FROM cmn.AccountBank WHERE Deleted = 0
