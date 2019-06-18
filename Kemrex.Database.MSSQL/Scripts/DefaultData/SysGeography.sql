IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tmp_SysGeography')
BEGIN
	DROP TABLE [tmp_SysGeography];
END
GO
CREATE TABLE [tmp_SysGeography] (
	[GeoId]		INT NOT NULL,
	[GeoNameTH]	NVARCHAR(256) NULL,
	[GeoNameEN]	NVARCHAR(256) NULL
);
GO
INSERT [tmp_SysGeography] ([GeoId], [GeoNameTH], [GeoNameEN]) VALUES (1, N'ภาคเหนือ', NULL);
INSERT [tmp_SysGeography] ([GeoId], [GeoNameTH], [GeoNameEN]) VALUES (2, N'ภาคกลาง', NULL);
INSERT [tmp_SysGeography] ([GeoId], [GeoNameTH], [GeoNameEN]) VALUES (3, N'ภาคตะวันออกเฉียงเหนือ', N'');
INSERT [tmp_SysGeography] ([GeoId], [GeoNameTH], [GeoNameEN]) VALUES (4, N'ภาคตะวันตก', NULL);
INSERT [tmp_SysGeography] ([GeoId], [GeoNameTH], [GeoNameEN]) VALUES (5, N'ภาคตะวันออก', NULL);
INSERT [tmp_SysGeography] ([GeoId], [GeoNameTH], [GeoNameEN]) VALUES (6, N'ภาคใต้', NULL);
GO
SET IDENTITY_INSERT [SysGeography] ON;
GO
MERGE INTO [SysGeography] AS trg USING (
	SELECT
		[GeoId],
		[GeoNameTH],
		[GeoNameEN]
	FROM [tmp_SysGeography]) AS src
ON trg.[GeoId] = src.[GeoId]
WHEN NOT MATCHED THEN
	INSERT ([GeoId], [GeoNameTH], [GeoNameEN])
	VALUES (src.[GeoId], src.[GeoNameTH], src.[GeoNameEN])
WHEN MATCHED THEN
	UPDATE SET
		trg.[GeoNameTH] = src.[GeoNameTH],
		trg.[GeoNameEN] = src.[GeoNameEN];
GO
SET IDENTITY_INSERT [SysGeography] OFF;
GO
DROP TABLE [tmp_SysGeography];
GO