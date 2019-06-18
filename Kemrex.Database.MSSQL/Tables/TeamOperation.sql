CREATE TABLE [dbo].[TeamOperation]
(
	[TeamId] INT NOT NULL IDENTITY(1, 1), 
    [TeamName] NVARCHAR(200) NOT NULL, 
    [ManagerId] BIGINT NOT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TeamOperation] PRIMARY KEY ([TeamId]), 
    CONSTRAINT [FK_TeamOperation_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [SysAccount]([AccountId]) 
)
