﻿CREATE TABLE [dbo].[TblEmployee]
(
	[EmpId] INT NOT NULL IDENTITY(1, 1), 
    [EmpCode] NVARCHAR(20) NOT NULL, 
    [AccountId] BIGINT NULL, 
    [PrefixId] INT NULL, 
	[EmpTypeId] INT NULL,
    [EmpNameTH] NVARCHAR(500) NOT NULL, 
    [EmpNameEN] NVARCHAR(500) NOT NULL, 
    [EmpPID] NVARCHAR(13) NULL, 
    [EmpMobile] NVARCHAR(10) NOT NULL, 
    [EmpEmail] NVARCHAR(250) NULL, 
	[DepartmentId] INT NULL,
	[PositionId] INT NULL,
    [LeadId] INT NULL, 
	[EmpApplyDate] DATETIME NULL,
	[EmpPromoteDate] DATETIME NULL,
    [EmpAddress] NVARCHAR(500) NULL, 
    [EmpPostcode] NVARCHAR(10) NULL, 
	[EmpSignature] NVARCHAR(300) NULL,
    [EmpRemark] NVARCHAR(MAX) NULL, 
	[StatusId] INT NOT NULL,
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblEmployee] PRIMARY KEY ([EmpId]), 
    CONSTRAINT [FK_TblEmployee_Account] FOREIGN KEY ([AccountId]) REFERENCES [SysAccount]([AccountId]), 
    CONSTRAINT [FK_TblEmployee_Prefix] FOREIGN KEY ([PrefixId]) REFERENCES [EnmPrefix]([PrefixId]), 
    CONSTRAINT [FK_TblEmployee_Status] FOREIGN KEY ([StatusId]) REFERENCES [EnmStatusEmployee]([StatusId]), 
    CONSTRAINT [FK_TblEmployee_Lead] FOREIGN KEY ([LeadId]) REFERENCES [TblEmployee]([EmpId]), 
    CONSTRAINT [UK_TblEmployee_Code] UNIQUE ([EmpCode]), 
    CONSTRAINT [FK_TblEmployee_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [TblDepartment]([DepartmentId]), 
    CONSTRAINT [FK_TblEmployee_Position] FOREIGN KEY ([PositionId]) REFERENCES [TblPosition]([PositionId])
)
