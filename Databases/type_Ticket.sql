
GO
CREATE TYPE cmn.[Ticket] AS TABLE(
	[Id] [int] NOT NULL,
	[IssueDate] DATETIME NULL,	
	TicketNo Varchar(20) NOT NULL,
	NoSeats Int NULL,
	FlyNo Varchar(10)  NULL,
	Brand NVARCHAR(100) NULL,
	Class NVARCHAR(100) NULL,
	Journey NVARCHAR(200) NULL,
	FlyDate DATE NULL,
	FlyTime varchar(8) NULL,
	FlyTime1 varchar(8) NULL,
	Passenger NVARCHAR(100) NOT NULL,
	DOB VARCHAR(10) NULL,
	Title NVARCHAR(20) NULL,
	NetPrice Money NOT NULL,
	CommissionL2 Money NOT NULL,
	ServiceFee MONEY NOT NULL ,
	PackageFee MONEY NOT NULL ,
	Diffirence MONEY NOT NULL ,
	Paid MONEY NOT NULL ,
	Remain MONEY NOT NULL ,
	TotalPrice MONEY NOT NULL,
	IsCancel BIT NOT NULL ,
	CancelFee MONEY NOT NULL ,
	ExtraFee MONEY NOT NULL,
	Refund MONEY NOT NULL ,
	IsChanged BIT NOT NULL ,
	ChangeFee MONEY NOT NULL ,
	Penalty MONEY NOT NULL,
	CustomerID INT NULL,
	XuatNgoai BIT,
	Note NVARCHAR(1000) NULL,
	IsClose BIT,
	Imported BIT	
)
GO


