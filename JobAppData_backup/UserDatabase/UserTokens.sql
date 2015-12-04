CREATE TABLE [dbo].[UserTokens]
(
	[GUID] CHAR(36) NOT NULL PRIMARY KEY, 
	[lastAccessed] DATETIME NOT NULL, 
    [UserType] INT NOT NULL DEFAULT 0, 
    [userID] INT NULL
)
