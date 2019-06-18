IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpSysPermission')
BEGIN
	DROP TABLE [TmpSysPermission];
END
GO
CREATE TABLE [TmpSysPermission] (
	[PermissionId] INT NOT NULL , 
    [PermissionName] NVARCHAR(100) NOT NULL, 
    [PermissionRemark] NVARCHAR(MAX) NULL
);
GO
INSERT INTO [TmpSysPermission] VALUES (1, N'View', N'สามารถดูข้อมูลได้');
INSERT INTO [TmpSysPermission] VALUES (2, N'Write', N'สามารถสร้าง/แก้ไขข้อมูลได้');
INSERT INTO [TmpSysPermission] VALUES (3, N'Delete', N'สามารถลบข้อมูลได้');
INSERT INTO [TmpSysPermission] VALUES (4, N'Export', N'สามารถดาวน์โหลดข้อมูลได้');
GO
MERGE INTO [SysPermission] AS trg USING (
	SELECT
		[PermissionId],
		[PermissionName],
		[PermissionRemark]
	FROM [TmpSysPermission]) AS src
ON trg.[PermissionId] = src.[PermissionId]
WHEN NOT MATCHED THEN
	INSERT (
		[PermissionId],
		[PermissionName],
		[PermissionRemark]
	) VALUES (
		src.[PermissionId],
		src.[PermissionName],
		src.[PermissionRemark])
WHEN MATCHED THEN
	UPDATE SET
		trg.[PermissionName] = src.[PermissionName],
		trg.[PermissionRemark] = src.[PermissionRemark];
GO
DROP TABLE [TmpSysPermission];
GO
PRINT N'Done...SysPermission'