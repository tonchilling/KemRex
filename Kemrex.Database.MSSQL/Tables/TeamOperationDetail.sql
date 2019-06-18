CREATE TABLE [dbo].[TeamOperationDetail]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
	[TeamId] INT NOT NULL,
	[AccountId] BIGINT NOT NULL,
	[TeamRemark] NVARCHAR(MAX) NULL,
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TeamOperationDetail] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TeamOperationDetail_Account] FOREIGN KEY ([AccountId]) REFERENCES [SysAccount]([AccountId]), 
    CONSTRAINT [FK_TeamOperationDetail_Id] FOREIGN KEY ([TeamId]) REFERENCES [TeamOperation]([TeamId])
)
