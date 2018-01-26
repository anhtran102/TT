

CREATE PROCEDURE [cmn].[AddTicket]	
	@Id [int] ,
	@IssueDate [datetime] ,
	@TicketNo [varchar](20) ,
	@Brand [nvarchar](100) ,
	@Class [nvarchar](100),
	@Journey [nvarchar](200) ,
	@FlyDate [date] ,
	@FlyTime [time](7) ,
	@FlyTime1 [time](7) ,
	@FlyTime2 [time](7) ,
	@Passenger [nvarchar](100) ,
	@DOB [datetime],
	@Title [nvarchar](20),
	@NetPrice [money] ,
	@CommissionL2 [money] ,
	@ServiceFee [money] ,
	@PackageFee [money] ,
	@Diffirence [money] ,
	@Paid [money] ,
	@Remain [money] ,
	@TotalPrice [money] ,
	@IsCancel [bit] ,
	@CancelFee [money] ,
	@Refund [money] ,
	@IsChanged [bit] ,
	@ChangeFee [money] ,
	@CustomerID [int],
	@IsTour [bit]	
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

BEGIN TRAN
	INSERT INTO cmn.Ticket
	(
			[IssueDate]
           ,[TicketNo]
           ,[Brand]
           ,[Class]
           ,[Journey]
           ,[FlyDate]
           ,[FlyTime]
           ,[FlyTime1]
           ,[FlyTime2]
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
           ,[Refund]
           ,[IsChanged]
           ,[ChangeFee]
           ,[CustomerID]
           ,[IsTour]
	) 
	VALUES(
			@IssueDate
           ,@TicketNo
           ,@Brand
           ,@Class
           ,@Journey
           ,@FlyDate
           ,@FlyTime
           ,@FlyTime1
           ,@FlyTime2
           ,@Passenger
           ,@DOB
           ,@Title
           ,@NetPrice
           ,@CommissionL2
           ,@ServiceFee
           ,@PackageFee
           ,@Diffirence
           ,@Paid
           ,@Remain
           ,@TotalPrice
           ,@IsCancel
           ,@CancelFee
           ,@Refund
           ,@IsChanged
           ,@ChangeFee
           ,@CustomerID
           ,@IsTour
	)
COMMIT TRAN

GO


