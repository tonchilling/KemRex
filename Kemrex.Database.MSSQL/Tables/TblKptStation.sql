CREATE TABLE [dbo].[TblKptStation]
(
	[StationId] INT NOT NULL IDENTITY(1, 1), 
    [KptId] INT NOT NULL, 
    [StationName] NVARCHAR(100) NOT NULL, 
	[StationTestBy] NVARCHAR(200) NOT NULL,
	[StationRemark] NVARCHAR(MAX) NULL,
    [StationOrder] INT NOT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblKptStation] PRIMARY KEY ([StationId]), 
    CONSTRAINT [FK_TblKptStation_Id] FOREIGN KEY ([KptId]) REFERENCES [TblKpt]([KptId]) 
)
