CREATE TABLE [dbo].[TblProductModel]
(
	[ModelId] INT NOT NULL IDENTITY(1, 1), 
    [CategoryId] INT NOT NULL, 
    [ModelName] NVARCHAR(200) NOT NULL, 
    [ModelOrder] INT NOT NULL, 
    [FlagActive] BIT NOT NULL CONSTRAINT DF_TblProductModel_FlagActive DEFAULT 1, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblProductModel] PRIMARY KEY ([ModelId]), 
    CONSTRAINT [FK_TblProductModel_Category] FOREIGN KEY ([CategoryId]) REFERENCES [TblProductCategory]([CategoryId])
)
