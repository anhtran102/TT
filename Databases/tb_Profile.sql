

/****** Object:  Table [cmn].[Profile]    Script Date: 4/7/2016 8:54:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE cmn.[Profile](
	[ProfileID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PrimaryRoleID] [int] NOT NULL,
	[CanManageAccountLocks] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[LastUpdated] [datetime2](7) NOT NULL
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE cmn.[Profile] ADD  CONSTRAINT [DF_SystemType_PrimaryRoleID]  DEFAULT ((-1)) FOR [PrimaryRoleID]
GO

ALTER TABLE cmn.[Profile] ADD  CONSTRAINT [DF_SystemType_CanManageAccountLocks]  DEFAULT ((0)) FOR [CanManageAccountLocks]
GO

ALTER TABLE cmn.[Profile] ADD  CONSTRAINT [DF_SystemType_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE cmn.[Profile] ADD  CONSTRAINT [DF_Profile_Created]  DEFAULT (sysdatetimeoffset()) FOR [Created]
GO

ALTER TABLE cmn.[Profile] ADD  CONSTRAINT [DF_Profile_LastUpdated]  DEFAULT (sysdatetimeoffset()) FOR [LastUpdated]
GO

ALTER TABLE cmn.[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_PrimaryRole] FOREIGN KEY([PrimaryRoleID])
REFERENCES cmn.[PrimaryRole] ([PrimaryRoleID])
GO

ALTER TABLE cmn.[Profile] CHECK CONSTRAINT [FK_Profile_PrimaryRole]
GO


