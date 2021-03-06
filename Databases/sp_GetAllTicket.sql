
CREATE PROCEDURE [cmn].[GetAllTicket]	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

SELECT [Id]
      ,[IssueDate]
      ,[TicketNo]
      ,[NoSeats]
      ,[FlyNo]
      ,[Brand]
      ,[Class]
      ,[Journey]
      ,[FlyDate]
      ,[FlyTime]
      ,[FlyTime1]
      ,[Passenger]
      ,[DOB]
      ,[Title]
      ,[NetPrice]
      ,[CommissionL2]
      ,[ServiceFee]
      ,[PackageFee]
      ,[Diffirence]
      ,[Paid]
      ,[Remain]
      ,[TotalPrice]
      ,[IsCancel]
      ,[CancelFee]
	  ,ExtraFee
      ,[Refund]
      ,[IsChanged]
      ,[ChangeFee]
	  ,Penalty
      ,[CustomerID]      
      ,[IsConfirm]
      ,[Deleted]
      ,[Created]
      ,[LastUpdated]
	  ,Note
	  ,XuatNgoai
  FROM [cmn].[Ticket] WHERE Deleted = 0
