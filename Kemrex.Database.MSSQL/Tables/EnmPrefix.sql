CREATE TABLE [dbo].[EnmPrefix]
(
	[PrefixId] INT NOT NULL, 
    [PrefixNameTH] NVARCHAR(100) NOT NULL, 
    [PrefixNameEN] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_EnmPrefix] PRIMARY KEY ([PrefixId]) 
)
