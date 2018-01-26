

CREATE TABLE [cmn].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL Primary Key,	
	[CustomerName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](1000) NULL,
	[Phone] [varchar](15) NULL,
	[Deleted] [bit] NOT NULL DEFAULT(0),
	[Created] [datetime2](7) NOT NULL DEFAULT(SysDateTime()),
	[LastUpdated] [datetime2](7) NOT NULL DEFAULT(SYSDATETIME())	
)