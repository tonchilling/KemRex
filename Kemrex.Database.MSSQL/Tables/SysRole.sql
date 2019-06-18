CREATE TABLE [dbo].[SysRole]
(
	[RoleId] INT NOT NULL IDENTITY(1, 1), 
	[SiteId] INT NOT NULL,
    [RoleName] NVARCHAR(150) NOT NULL, 
    [RoleDescription] NVARCHAR(MAX) NULL, 
	[FlagSystem] BIT NOT NULL DEFAULT 0,
	[FlagActive] BIT NOT NULL DEFAULT 1,
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_SysRole] PRIMARY KEY ([RoleId]), 
    CONSTRAINT [FK_SysRole_Site] FOREIGN KEY ([SiteId]) REFERENCES [SysSite]([SiteId])
)

GO

CREATE INDEX [IX_SysRole_Site] ON [dbo].[SysRole] ([SiteId])
