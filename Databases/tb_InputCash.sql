
CREATE TABLE cmn.InputCash(
	Id [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Cash Money NOT NULL DEFAULT(0),
	FromAccount VARCHAR(20) NULL FOREIGN kEY REFERENCES cmn.AccountBank(AccountNo),
	ToAccount VARCHAR(20) NULL FOREIGN kEY REFERENCES cmn.AccountBank(AccountNo),
	Payer NVARCHAR(100) NULL,
	PayDate DATETIME DEFAULT(SYSDATETIME()),
	[Deleted] [bit] NOT NULL  DEFAULT (0),
	[Created] [datetime2](7) NOT NULL  DEFAULT (sysdatetimeoffset()),
	[LastUpdated] [datetime2](7) NOT NULL  DEFAULT (sysdatetimeoffset())	
)

ALTER TABLE cmn.InputCash add IsOwn BIT NOT NULL DEFAULT(0)
ALTER TABLE cmn.InputCash add CustomerID INT NULL

ALTER TABLE cmn.InputCash Add Constraint FK_Cash_Customer FOREIGN KEY (CustomerID) REFERENCES cmn.Customer(Id)