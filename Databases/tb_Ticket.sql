
CREATE TABLE [cmn].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IssueDate] DATETIME NULL,	
	TicketNo Varchar(20) NOT NULL,
	NoSeats Int NULL,
	FlyNo Varchar(10) NULL,
	Brand NVARCHAR(100) NULL,
	Class NVARCHAR(100) NULL,
	Journey NVARCHAR(200) NULL,
	FlyDate DATE NULL,
	FlyTime varchar(8) NULL,
	FlyTime1 varchar(8) NULL,
	Passenger NVARCHAR(100) NOT NULL,
	DOB DATETIME NULL,
	Title NVARCHAR(20) NULL,
	NetPrice Money NOT NULL,
	CommissionL2 Money NOT NULL DEFAULT(0),
	ServiceFee MONEY NOT NULL DEFAULT(0),
	PackageFee MONEY NOT NULL DEFAULT(0),
	Diffirence MONEY NOT NULL DEFAULT(0),
	Paid MONEY NOT NULL DEFAULT(0),
	Remain MONEY NOT NULL DEFAULT(0),
	TotalPrice MONEY NOT NULL,
	IsCancel BIT NOT NULL DEFAULT(0),
	CancelFee MONEY NOT NULL DEFAULT(0),
	ExtraFee MONEY NOT NULL DEFAULT(0),
	Refund MONEY NOT NULL DEFAULT(0),
	IsChanged BIT NOT NULL DEFAULT(0),
	ChangeFee MONEY NOT NULL DEFAULT(0),
	Penalty MONEY NOT NULL DEFAULT(0),
	CustomerID INT NULL,
	XuatNgoai BIT NOT NULL DEFAULT(0),
	IsConfirm BIT NOT NULL DEFAULT(0),
	Note NVARCHAR(1000) NULL,
	IsClose BIT DEFAULT(0),
	[Deleted] [bit] NOT NULL,
	[Created] [datetime2](7) NOT NULL DEFAULT(SYSDATETIME()),
	[LastUpdated] [datetime2](7) NOT NULL DEFAULT(SYSDATETIME())	
)

ALTER TABLE [cmn].[Ticket] Add Constraint PK_Ticket Primary key(Id)
ALTER TABLE [cmn].[Ticket] Add Constraint FK_Ticket_Customer FOREIGN KEY(CustomerID) REFERENCES cmn.Customer(Id)

CREATE NONCLUSTERED INDEX [IX_Ticket] ON cmn.[Ticket](TicketNo)
