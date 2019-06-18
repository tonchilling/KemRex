CREATE TABLE [dbo].[TeamSale]
(
	[TeamId] INT NOT NULL IDENTITY(1, 1), 
    [TeamName] NVARCHAR(200) NOT NULL, 
    [ManagerId] BIGINT NOT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TeamSale] PRIMARY KEY ([TeamId]), 
    CONSTRAINT [FK_TeamSale_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [SysAccount]([AccountId]) 
)
