CREATE TABLE [dbo].[SysAccountRole]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[AccountId] BIGINT NOT NULL, 
    [RoleId] INT NOT NULL, 
    CONSTRAINT [PK_SysAccountRole] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_SysAccountRole_User] FOREIGN KEY ([AccountId]) REFERENCES [SysAccount]([AccountId]), 
    CONSTRAINT [FK_SysAccountRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [SysRole]([RoleId])
)
