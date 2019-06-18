SET IDENTITY_INSERT SysRole ON;
GO
IF NOT EXISTS(SELECT * FROM SysRole WHERE RoleId = 1)
BEGIN
	INSERT INTO SysRole (RoleId, SiteId, RoleName, RoleDescription, FlagSystem, FlagActive, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
	VALUES (1, 3, N'ผู้ดูแลระบบ (Calc)', N'Calculator Application Administrator', 1, 1, 1, GETDATE(), 1, GETDATE());
END
IF NOT EXISTS(SELECT * FROM SysRole WHERE RoleId = 200)
BEGIN
	INSERT INTO SysRole (RoleId, SiteId, RoleName, RoleDescription, FlagSystem, FlagActive, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
	VALUES (200, 2, N'ผู้ดูแลระบบ', N'Administrator', 1, 1, 1, GETDATE(), 1, GETDATE());
END
GO
SET IDENTITY_INSERT SysRole OFF;
GO
MERGE INTO [SysRolePermission] AS trg USING (
	SELECT
		1 AS RoleId,
		a.MenuId,
		a.PermissionId
	FROM [SysMenuPermission] a
	JOIN [SysMenu] b ON a.MenuId = b.MenuId
	WHERE b.SiteId = 3) AS src
ON
	trg.[MenuId] = src.[MenuId]
	AND trg.[PermissionId] = src.[PermissionId]
	AND trg.[RoleId] = src.[RoleId]
	
WHEN NOT MATCHED THEN
	INSERT (
		[RoleId],
		[MenuId],
		[PermissionId],
		[PermissionFlag]
	) VALUES (
		src.[RoleId],
		src.[MenuId],
		src.[PermissionId],
		1)
WHEN MATCHED THEN
	UPDATE SET trg.[PermissionFlag] = 1;
GO
MERGE INTO [SysRolePermission] AS trg USING (
	SELECT
		200 AS RoleId,
		a.[MenuId],
		a.[PermissionId]
	FROM [SysMenuPermission] a
	JOIN [SysMenu] b ON a.MenuId = b.MenuId
	WHERE b.SiteId = 2) AS src
ON
	trg.[MenuId] = src.[MenuId]
	AND trg.[PermissionId] = src.[PermissionId]
	AND trg.[RoleId] = src.[RoleId]
	
WHEN NOT MATCHED THEN
	INSERT (
		[RoleId],
		[MenuId],
		[PermissionId],
		[PermissionFlag]
	) VALUES (
		src.[RoleId],
		src.[MenuId],
		src.[PermissionId],
		1)
WHEN MATCHED THEN
	UPDATE SET trg.[PermissionFlag] = 1;
GO
IF NOT EXISTS(SELECT * FROM SysAccountRole WHERE AccountId = 1 AND RoleId = 1)
BEGIN
	INSERT INTO SysAccountRole (AccountId, RoleId) VALUES (1, 1);
END
IF NOT EXISTS(SELECT * FROM SysAccountRole WHERE AccountId = 1 AND RoleId = 200)
BEGIN
	INSERT INTO SysAccountRole (AccountId, RoleId) VALUES (1, 200);
END
GO