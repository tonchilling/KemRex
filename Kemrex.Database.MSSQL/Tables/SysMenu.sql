CREATE TABLE [dbo].[SysMenu]
(
	[MenuId] INT NOT NULL, 
	[SiteId] INT NOT NULL,
    [MenuLevel] INT NOT NULL,
    [MenuName] NVARCHAR(100) NOT NULL, 
    [MenuIcon] NVARCHAR(500) NULL, 
    [MenuOrder] INT NOT NULL, 
    [ParentId] INT NULL, 
    [MvcArea] NVARCHAR(100) NOT NULL, 
    [MvcController] NVARCHAR(100) NOT NULL, 
    [MvcAction] NVARCHAR(100) NOT NULL, 
	[FlagActive] BIT NOT NULL DEFAULT 1,
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_SysMenu] PRIMARY KEY ([MenuId]), 
    CONSTRAINT [FK_SysMenu_Parent] FOREIGN KEY ([ParentId]) REFERENCES [SysMenu]([MenuId]), 
    CONSTRAINT [FK_SysMenu_Site] FOREIGN KEY ([SiteId]) REFERENCES [SysSite]([SiteId]) 
)
