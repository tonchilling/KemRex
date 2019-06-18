CREATE TABLE [dbo].[TblDepartmentPosition]
(
	[PositionId] INT NOT NULL IDENTITY(1, 1), 
    [DepartmentId] INT NOT NULL, 
    [PositionName] NVARCHAR(200) NOT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblDepartmentPosition] PRIMARY KEY ([PositionId]), 
    CONSTRAINT [FK_TblDepartmentPosition_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [TblDepartment]([DepartmentId]) 
)
