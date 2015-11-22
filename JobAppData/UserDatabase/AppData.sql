CREATE TABLE [dbo].[AppData]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ssn] CHAR(11) NOT NULL,
	[firstname] VARCHAR(50) NOT NULL, 
    [lastname] VARCHAR(50) NOT NULL, 
    [phone] CHAR(10) NOT NULL, 
    [date_submitted] DATETIME NOT NULL,
    
)
