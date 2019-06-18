CREATE TABLE [dbo].[TblKptDetail]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1), 
    [KptId] INT NOT NULL, 
    [KptDepth] INT NOT NULL, 
    [BlowCount] INT NOT NULL, 
    CONSTRAINT [PK_TblKptDetail] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TblKptDetail_Id] FOREIGN KEY ([KptId]) REFERENCES [TblKpt]([KptId]) 
)
