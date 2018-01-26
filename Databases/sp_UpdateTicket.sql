

CREATE PROCEDURE [cmn].[UpdateTicket]	
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
	UPDATE cmn.Ticket
	SET
			IssueDate = @IssueDate
           ,TicketNo = @TicketNo
           ,Brand = @Brand
           ,Class= @Class
           ,Journey = @Journey
           ,FlyDate= @FlyDate
           ,FlyTime = @FlyTime
           ,FlyTime1 = @FlyTime1
           ,FlyTime2= @FlyTime2
           ,Passenger = @Passenger
           ,DOB= @DOB
           ,Title = @Title
           ,NetPrice = @NetPrice
           ,CommissionL2 = @CommissionL2
           ,ServiceFee = @ServiceFee
           ,PackageFee= @PackageFee
           ,Diffirence = @Diffirence
           ,Paid = @Paid
           ,Remain = @Remain
           ,TotalPrice = @TotalPrice
           ,IsCancel = @IsCancel
           ,CancelFee = @CancelFee
           ,Refund = @Refund
           ,IsChanged = @IsChanged
           ,ChangeFee = @ChangeFee
           ,CustomerID = @CustomerID
           ,IsTour = @IsTour
		   ,LastUpdated = SYSDATETIME()
	
COMMIT TRAN

GO


