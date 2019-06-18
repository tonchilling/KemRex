CREATE TABLE [dbo].[SysPermission]
(
	[PermissionId] INT NOT NULL , 
    [PermissionName] NVARCHAR(100) NOT NULL, 
    [PermissionRemark] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_SysPermission] PRIMARY KEY ([PermissionId])
)
