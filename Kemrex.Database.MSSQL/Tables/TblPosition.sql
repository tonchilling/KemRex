CREATE TABLE [dbo].[TblPosition]
(
	[PositionId] INT NOT NULL IDENTITY(1, 1), 
    [PositionName] NVARCHAR(200) NOT NULL ,
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblPosition] PRIMARY KEY ([PositionId]), 
)
