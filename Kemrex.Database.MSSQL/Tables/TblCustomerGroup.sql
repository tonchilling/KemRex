CREATE TABLE [dbo].[TblCustomerGroup]
(
	[GroupId] INT NOT NULL IDENTITY(1, 1), 
    [GroupName] NVARCHAR(200) NOT NULL, 
    [GroupOrder] INT NOT NULL DEFAULT 9999, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblCustomerGroup] PRIMARY KEY ([GroupId]) 
)
