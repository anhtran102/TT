
/****** Object:  Table [cmn].[User]    Script Date: 4/7/2016 8:53:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE cmn.[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[ProfileID] [int] NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[PasswordAttemptCount] [int] NOT NULL,
	[LockTime] [datetime] NULL,	
	[PreferredLanguage] [nvarchar](8) NULL,			
	[Deleted] [bit] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[LastUpdated] [datetime2](7) NOT NULL	
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_User_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE cmn.[User] ADD  CONSTRAINT [DF_User_PasswordAttemptCount]  DEFAULT ((0)) FOR [PasswordAttemptCount]
GO

ALTER TABLE cmn.[User] ADD  CONSTRAINT [DF_User_PreferredLanguage]  DEFAULT ('en-US') FOR [PreferredLanguage]
GO

ALTER TABLE cmn.[User] ADD  CONSTRAINT [DF_User_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE cmn.[User] ADD  CONSTRAINT [DF_User_Created]  DEFAULT (sysdatetimeoffset()) FOR [Created]
GO

ALTER TABLE cmn.[User] ADD  CONSTRAINT [DF_User_LastUpdated]  DEFAULT (sysdatetimeoffset()) FOR [LastUpdated]
GO

ALTER TABLE cmn.[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Profile] FOREIGN KEY([ProfileID])
REFERENCES cmn.[Profile] ([ProfileID])
GO

ALTER TABLE cmn.[User] CHECK CONSTRAINT [FK_User_Profile]
GO



