CREATE TABLE [dbo].[SysRolePermission]
(
    [RoleId] INT NOT NULL, 
	[MenuId] INT NOT NULL,
    [PermissionId] INT NOT NULL, 
    [PermissionFlag] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_SysRolePermission] PRIMARY KEY ([RoleId], [MenuId], [PermissionId]), 
    CONSTRAINT [FK_SysRolePermission_Id] FOREIGN KEY ([RoleId]) REFERENCES [SysRole]([RoleId]), 
    CONSTRAINT [FK_SysRolePermission_Menu] FOREIGN KEY ([MenuId]) REFERENCES [SysMenu]([MenuId]), 
    CONSTRAINT [FK_SysRolePermission_Detail] FOREIGN KEY ([PermissionId]) REFERENCES [SysPermission]([PermissionId])
)
