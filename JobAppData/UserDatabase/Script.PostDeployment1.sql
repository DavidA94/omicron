/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[Users] ON

DELETE FROM [dbo].[Users];

INSERT INTO [dbo].[Users] (Id, username, password, UserType)
VALUES
(0, 'admin', '123456', 1),
(1, 'Random', 'randompass', 0),
(2, 'Hatim', 'hatimpass', 0),
(3, 'David', 'davidpass', 0);

DELETE FROM [dbo].[AppData];

INSERT INTO [dbo].[AppData] (Id, ssn, firstname, lastname, phone, date_submitted)
VALUES
(1, '555-55-5555', 'Random', 'Davis', '5035151798', '2015-12-01 12:00:00'),
(2, '666-66-6666', 'Hatim', 'Painter', '5035555555', '2015-12-03 12:00:00'),
(3, '777-77-7777', 'David', 'Antonucci', '5035551234', '2015-12-02 12:00:00')
;

SET IDENTITY_INSERT [dbo].[Users] OFF