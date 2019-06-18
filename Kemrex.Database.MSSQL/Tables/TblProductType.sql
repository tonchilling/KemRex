CREATE TABLE [dbo].[TblProductType]
(
	[TypeId] INT NOT NULL IDENTITY(1, 1), 
    [TypeName] NVARCHAR(200) NOT NULL, 
    [TypeDetail] NVARCHAR(MAX) NULL, 
    [TypeOrder] INT NOT NULL, 
    [FlagActive] BIT NOT NULL CONSTRAINT DF_TblProductType_FlagActive DEFAULT 1, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblProductType] PRIMARY KEY ([TypeId])
)
