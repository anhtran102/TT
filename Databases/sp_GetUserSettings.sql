USE [MASTER_TIEPTUYEN]
GO
/****** Object:  StoredProcedure [cmn].[GetUserSettings]    Script Date: 4/18/2016 2:06:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [cmn].[GetUserSettings]
	@UserName		NVARCHAR(100),
	@Password NVARCHAR(100)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET NOCOUNT ON

SELECT		u.UserID, u.UserName, u.PreferredLanguage, p.PrimaryRoleID, u.ProfileID, p.CanManageAccountLocks,
			LockTime, u.Locked			
FROM		cmn.[User] u
INNER JOIN	cmn.[Profile] p ON p.ProfileID = u.ProfileID
WHERE		UserName = @UserName AND Password = @Password 
AND			u.Deleted != 1
