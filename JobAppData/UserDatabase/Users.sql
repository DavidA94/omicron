CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL  IDENTITY, 
    [username] VARCHAR(15) NOT NULL, 
    [password] VARCHAR(50) NOT NULL, 
    [UserType] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([username]) 
)
