CREATE TABLE [dbo].[SysParameter]
(
    [ParamName] NVARCHAR(100) NOT NULL, 
    [ParamValue] NVARCHAR(MAX) NOT NULL, 
    [ParamType] NVARCHAR(50) NOT NULL, 
    [ParamLength] INT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_SysParameter] PRIMARY KEY ([ParamName])
)
