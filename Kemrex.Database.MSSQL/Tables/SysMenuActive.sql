CREATE TABLE [dbo].[SysMenuActive]
(
	[Id] INT NOT NULL, 
    [MvcArea] NVARCHAR(100) NOT NULL, 
    [MvcController] NVARCHAR(100) NOT NULL, 
    [MvcAction] NVARCHAR(100) NOT NULL, 
    [MenuId] INT NOT NULL, 
    CONSTRAINT [PK_SysMenuActive] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_SysMenuActive_Id] FOREIGN KEY ([MenuId]) REFERENCES [SysMenu]([MenuId]) 
)
