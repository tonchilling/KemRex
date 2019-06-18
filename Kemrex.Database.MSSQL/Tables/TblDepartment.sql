CREATE TABLE [dbo].[TblDepartment]
(
	[DepartmentId] INT NOT NULL IDENTITY(1, 1), 
    [DepartmentName] NVARCHAR(200) NOT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblDepartment] PRIMARY KEY ([DepartmentId]) 
)
