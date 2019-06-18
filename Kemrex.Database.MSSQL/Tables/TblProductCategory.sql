CREATE TABLE [dbo].[TblProductCategory]
(
	[CategoryId] INT NOT NULL IDENTITY(1, 1), 
    [CategoryName] NVARCHAR(200) NOT NULL, 
    [CategoryDetail] NVARCHAR(MAX) NULL, 
    [CategoryOrder] INT NOT NULL, 
    [FlagActive] BIT NOT NULL CONSTRAINT DF_TblProductCategory_FlagActive DEFAULT 1, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblProductCategory] PRIMARY KEY ([CategoryId])
)
