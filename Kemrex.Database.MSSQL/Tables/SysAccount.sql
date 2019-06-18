CREATE TABLE [dbo].[SysAccount]
(
	[AccountId] BIGINT NOT NULL IDENTITY(1, 1),
    [AccountAvatar] NVARCHAR(MAX) NULL, 
    [AccountUsername] NVARCHAR(100) NOT NULL, 
    [AccountPassword] NVARCHAR(100) NULL, 
	[AccountFirstName] NVARCHAR(200) NOT NULL,
	[AccountLastName] NVARCHAR(200) NULL,
	[AccountEmail] NVARCHAR(200) NOT NULL,
	[AccountRemark] NVARCHAR(MAX) NULL,
    [FlagStatus] INT NOT NULL DEFAULT 1, 
    [FlagSystem] BIT NOT NULL DEFAULT 0,
	[FlagAdminCalc] BIT NOT NULL DEFAULT 0,
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_SysAccount] PRIMARY KEY ([AccountId])
)
GO
CREATE INDEX [IX_SysAccount_Username] ON [dbo].[SysAccount] ([AccountUsername])
GO
CREATE INDEX [IX_SysAccount_Email] ON [dbo].[SysAccount] ([AccountEmail])