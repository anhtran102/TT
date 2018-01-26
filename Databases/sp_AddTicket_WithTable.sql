

CREATE PROCEDURE [cmn].[AddTicket]	
	@Tickets cmn.Ticket READONLY	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	INSERT INTO cmn.Ticket
	(
			[IssueDate]
           ,[TicketNo]
		   ,NoSeats
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
		   , ExtraFee	   
           ,[Refund]
           ,[IsChanged]
           ,[ChangeFee]
		   ,Penalty
           ,[CustomerID]		   
		   ,Deleted
		   ,Note		             
	) 
	SELECT 
			[IssueDate]
           ,[TicketNo]
		   ,NoSeats
		   ,FlyNo
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
           ,CASE WHEN [CustomerID]  = 0 THEN NULL ELSE [CustomerID] END		   
		   ,0       
		   ,Note    
	FROM @Tickets		
COMMIT TRAN

GO


