

CREATE TABLE cmn.AccountBank
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	AccountNo VARCHAR(20) NOT NULL UNIQUE,
	AccountName NVARCHAR(100) NOT NULL,
	BankName NVARCHAR(100) NOT NULL,
	Phone VARCHAR(15) NULL,			
	[Deleted] [bit] NOT NULL  DEFAULT (0),
	[Created] [datetime2](7) NOT NULL  DEFAULT (sysdatetimeoffset()),
	[LastUpdated] [datetime2](7) NOT NULL  DEFAULT (sysdatetimeoffset()),
	IsOwn BIT NOT NULL DEFAULT(0)
)

