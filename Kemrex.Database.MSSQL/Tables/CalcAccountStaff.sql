CREATE TABLE [dbo].[CalcAccountStaff]
(
	[AccountId] BIGINT NOT NULL, 
    [StaffId] BIGINT NOT NULL, 
    [StaffRemark] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_CalcAccountStaff] PRIMARY KEY ([AccountId], [StaffId]), 
    CONSTRAINT [FK_CalcAccountStaff_Account] FOREIGN KEY ([AccountId]) REFERENCES [SysAccount]([AccountId]), 
    CONSTRAINT [FK_CalcAccountStaff_Staff] FOREIGN KEY ([StaffId]) REFERENCES [SysAccount]([AccountId])
)
