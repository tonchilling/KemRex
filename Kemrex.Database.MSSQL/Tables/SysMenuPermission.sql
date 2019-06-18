CREATE TABLE [dbo].[SysMenuPermission]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[MenuId] INT NOT NULL , 
    [PermissionId] INT NOT NULL, 
    CONSTRAINT [PK_SysMenuPermission] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_SysMenuPermission_Menu] FOREIGN KEY ([MenuId]) REFERENCES [SysMenu]([MenuId]), 
    CONSTRAINT [FK_SysMenuPermission_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [SysPermission]([PermissionId])
)
