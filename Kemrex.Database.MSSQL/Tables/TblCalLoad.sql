CREATE TABLE [dbo].[TblCalLoad]
(
	[CalId] INT NOT NULL IDENTITY(1, 1), 
	[ProjectName] NVARCHAR(200) NULL,
	[CalRemark] NVARCHAR(MAX) NULL,
	[InputC] DECIMAL(12,4) NOT NULL,
	[InputDegree] DECIMAL(12, 4) NOT NULL,
	[InputSafeLoad] DECIMAL(12, 4) NOT NULL,
	[ModelId] INT NOT NULL,
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblCalLoad] PRIMARY KEY ([CalId])
)
