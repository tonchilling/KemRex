IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpSysSite')
BEGIN
	DROP TABLE [TmpSysSite];
END
GO
CREATE TABLE [TmpSysSite] (
	[SiteId] INT NOT NULL, 
    [SiteName] NVARCHAR(100) NOT NULL, 
    [SiteDetail] NVARCHAR(MAX) NULL
);
GO
INSERT INTO [TmpSysSite] VALUES (1, N'System', N'ข้อมูลหลักของระบบ, สำหรับนักพัฒนาระบบหรือ Super administrator เท่านั้น');
INSERT INTO [TmpSysSite] VALUES (2, N'Main', N'ระบบจัดการข้อมูล');
INSERT INTO [TmpSysSite] VALUES (3, N'Calculator', N'แอพคำนวณค่ารับแรง');
GO
SET IDENTITY_INSERT [SysSite] ON;
GO
MERGE INTO [SysSite] AS trg USING (
	SELECT
		[SiteId],
		[SiteName],
		[SiteDetail]
	FROM [TmpSysSite]) AS src
ON trg.[SiteId] = src.[SiteId]
WHEN NOT MATCHED THEN
	INSERT (
		[SiteId],
		[SiteName],
		[SiteDetail]
	) VALUES (
		src.[SiteId],
		src.[SiteName],
		src.[SiteDetail])
WHEN MATCHED THEN
	UPDATE SET
		trg.[SiteName] = src.[SiteName],
		trg.[SiteDetail] = src.[SiteDetail];
GO
SET IDENTITY_INSERT [SysSite] OFF;
GO
DROP TABLE [TmpSysSite];
GO