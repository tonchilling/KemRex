CREATE TABLE [dbo].[TblUnit]
(
	[UnitId] INT NOT NULL IDENTITY(1, 1), 
    [UnitName] NVARCHAR(100) NOT NULL, 
    [UnitDetail] NVARCHAR(MAX) NULL, 
    [FlagActive] BIT NOT NULL CONSTRAINT DF_TblUnit_FlagActive DEFAULT 1, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblUnit] PRIMARY KEY ([UnitId])
)
