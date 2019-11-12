/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/* Run First time only 
--------------------------------------------------------------------------------------
*/
/* :r .\DefaultData\SysGeography.sql */
/* :r .\DefaultData\SysState.sql */
/* :r .\DefaultData\SysDistrict.sql */
/* :r .\DefaultData\SysSubDistrict.sql */
/*
--------------------------------------------------------------------------------------
*/
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpEnmStatusEmployee')
BEGIN
	DROP TABLE [TmpEnmStatusEmployee];
END
GO
CREATE TABLE [TmpEnmStatusEmployee] (
	[StatusId] INT NOT NULL, 
    [StatusName] NVARCHAR(100) NOT NULL, 
    [StatusOrder] INT NOT NULL
);
GO
INSERT INTO [TmpEnmStatusEmployee] VALUES (1, N'ปกติ', 1);
INSERT INTO [TmpEnmStatusEmployee] VALUES (2, N'ระงับ', 2);
INSERT INTO [TmpEnmStatusEmployee] VALUES (3, N'ลาออก', 3);
GO
MERGE INTO [EnmStatusEmployee] AS trg USING (
	SELECT
		[StatusId],
		[StatusName],
		[StatusOrder]
	FROM [TmpEnmStatusEmployee]) AS src
ON trg.[StatusId] = src.[StatusId]
WHEN NOT MATCHED THEN
	INSERT (
		[StatusId],
		[StatusName],
		[StatusOrder]
	) VALUES (
		src.[StatusId],
		src.[StatusName],
		src.[StatusOrder])
WHEN MATCHED THEN
	UPDATE SET
		trg.[StatusName] = src.[StatusName],
		trg.[StatusOrder] = src.[StatusOrder];
GO
DROP TABLE [TmpEnmStatusEmployee];
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpEnmStatusQuotation')
BEGIN
	DROP TABLE [TmpEnmStatusQuotation];
END
GO
CREATE TABLE [TmpEnmStatusQuotation] (
	[StatusId] INT NOT NULL, 
    [StatusName] NVARCHAR(100) NOT NULL, 
    [StatusOrder] INT NOT NULL
);
GO
INSERT INTO [TmpEnmStatusQuotation] VALUES (1, N'ร่าง', 1);
INSERT INTO [TmpEnmStatusQuotation] VALUES (2, N'ส่งแล้ว', 2);
INSERT INTO [TmpEnmStatusQuotation] VALUES (3, N'ตอบรับแล้ว', 3);
INSERT INTO [TmpEnmStatusQuotation] VALUES (4, N'ยกเลิก', 4);
GO
MERGE INTO [EnmStatusQuotation] AS trg USING (
	SELECT
		[StatusId],
		[StatusName],
		[StatusOrder]
	FROM [TmpEnmStatusQuotation]) AS src
ON trg.[StatusId] = src.[StatusId]
WHEN NOT MATCHED THEN
	INSERT (
		[StatusId],
		[StatusName],
		[StatusOrder]
	) VALUES (
		src.[StatusId],
		src.[StatusName],
		src.[StatusOrder])
WHEN MATCHED THEN
	UPDATE SET
		trg.[StatusName] = src.[StatusName],
		trg.[StatusOrder] = src.[StatusOrder];
GO
DROP TABLE [TmpEnmStatusQuotation];
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpEnmPrefix')
BEGIN
	DROP TABLE [TmpEnmPrefix];
END
GO
CREATE TABLE [TmpEnmPrefix] (
	[PrefixId] INT NOT NULL, 
    [PrefixNameTH] NVARCHAR(100) NOT NULL, 
    [PrefixNameEN] NVARCHAR(100) NOT NULL
);
GO
INSERT INTO [TmpEnmPrefix] VALUES (1, N'นาย', N'Mr.');
INSERT INTO [TmpEnmPrefix] VALUES (2, N'นาง', N'Mrs.');
INSERT INTO [TmpEnmPrefix] VALUES (3, N'นางสาว', N'Miss');
GO
MERGE INTO [EnmPrefix] AS trg USING (
	SELECT
		[PrefixId],
		[PrefixNameTH],
		[PrefixNameEN]
	FROM [TmpEnmPrefix]) AS src
ON trg.[PrefixId] = src.[PrefixId]
WHEN NOT MATCHED THEN
	INSERT (
		[PrefixId],
		[PrefixNameTH],
		[PrefixNameEN]
	) VALUES (
		src.[PrefixId],
		src.[PrefixNameTH],
		src.[PrefixNameEN])
WHEN MATCHED THEN
	UPDATE SET
		trg.[PrefixNameTH] = src.[PrefixNameTH],
		trg.[PrefixNameEN] = src.[PrefixNameEN];
GO
DROP TABLE [TmpEnmPrefix];
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpEnmPaymentCondition')
BEGIN
	DROP TABLE [TmpEnmPaymentCondition];
END
GO
CREATE TABLE [TmpEnmPaymentCondition] (
	[ConditionId] INT NOT NULL, 
    [ConditionName] NVARCHAR(100) NOT NULL, 
    [ConditionTerm] INT NOT NULL DEFAULT 1, 
    [FlagActive] BIT NOT NULL DEFAULT 1
);
GO
INSERT INTO [TmpEnmPaymentCondition] VALUES (1, N'จ่าย 100% หลังติดตั้งงาน', 1, 1);
INSERT INTO [TmpEnmPaymentCondition] VALUES (2, N'มัดจำ 100% ก่อนติดตั้งงาน', 1, 1);
INSERT INTO [TmpEnmPaymentCondition] VALUES (3, N'2 งวด 50/50', 2, 1);
GO
MERGE INTO [EnmPaymentCondition] AS trg USING (
	SELECT
		[ConditionId],
		[ConditionName],
		[ConditionTerm],
		[FlagActive]
	FROM [TmpEnmPaymentCondition]) AS src
ON trg.[ConditionId] = src.[ConditionId]
WHEN NOT MATCHED THEN
	INSERT (
		[ConditionId],
		[ConditionName],
		[ConditionTerm],
		[FlagActive]
	) VALUES (
		src.[ConditionId],
		src.[ConditionName],
		src.[ConditionTerm],
		src.[FlagActive])
WHEN MATCHED THEN
	UPDATE SET
		trg.[ConditionName] = src.[ConditionName],
		trg.[ConditionTerm] = src.[ConditionTerm],
		trg.[FlagActive] = src.[FlagActive];
GO
DROP TABLE [TmpEnmPaymentCondition];
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpEnmPaymentConditionTerm')
BEGIN
	DROP TABLE [TmpEnmPaymentConditionTerm];
END
GO
CREATE TABLE [TmpEnmPaymentConditionTerm] (
    [ConditionId] INT NOT NULL, 
    [TermNo] INT NOT NULL, 
    [TermPercent] INT NOT NULL, 
    [TermAmount] INT NOT NULL, 
    [FlagLast] BIT NOT NULL
);
GO
INSERT INTO [TmpEnmPaymentConditionTerm] VALUES (1, 1, 100, 0, 1);
INSERT INTO [TmpEnmPaymentConditionTerm] VALUES (2, 1, 100, 0, 1);
INSERT INTO [TmpEnmPaymentConditionTerm] VALUES (3, 1, 50, 0, 0);
INSERT INTO [TmpEnmPaymentConditionTerm] VALUES (3, 2, 50, 0, 1);
GO
MERGE INTO [EnmPaymentConditionTerm] AS trg USING (
	SELECT
		[ConditionId],
		[TermNo],
		[TermPercent],
		[TermAmount],
		[FlagLast]
	FROM [TmpEnmPaymentConditionTerm]) AS src
ON trg.[ConditionId] = src.[ConditionId] AND trg.[TermNo] = src.[TermNo]
WHEN NOT MATCHED THEN
	INSERT (
		[ConditionId],
		[TermNo],
		[TermPercent],
		[TermAmount],
		[FlagLast]
	) VALUES (
		src.[ConditionId],
		src.[TermNo],
		src.[TermPercent],
		src.[TermAmount],
		src.[FlagLast])
WHEN MATCHED THEN
	UPDATE SET
		trg.[TermPercent] = src.[TermPercent],
		trg.[TermAmount] = src.[TermAmount],
		trg.[FlagLast] = src.[FlagLast];
GO
DROP TABLE [TmpEnmPaymentConditionTerm];
GO

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
PRINT N'Configuration default value for parameter';
SET NOCOUNT ON;
BEGIN TRY
	BEGIN TRANSACTION;
	DECLARE @paramName NVARCHAR(100);
	SET @paramName = N'PaginationPageSize';
	PRINT N'Parameter: ' + @paramName;
	IF NOT EXISTS(SELECT 1 FROM SysParameter WHERE ParamName = @paramName)
	BEGIN
		INSERT INTO SysParameter (ParamName, ParamValue, ParamType, ParamLength, UpdatedBy, UpdatedDate) VALUES (@paramName, N'10,30,50,100', N'list-int', NULL, 1, GETDATE());
	END
	SET @paramName = N'SiteLogo';
	PRINT N'Parameter: ' + @paramName;
	IF NOT EXISTS(SELECT 1 FROM SysParameter WHERE ParamName = @paramName)
	BEGIN
		INSERT INTO SysParameter (ParamName, ParamValue, ParamType, ParamLength, UpdatedBy, UpdatedDate) VALUES (@paramName, N'images/logo.png', N'string', NULL, 1, GETDATE());
	END
	COMMIT;
	PRINT N'Done '
END TRY
BEGIN CATCH
	PRINT N'Error ' + ERROR_MESSAGE()
	ROLLBACK;
END CATCH
GO
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
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpSysMenu')
BEGIN
	DROP TABLE [TmpSysMenu];
END
GO
CREATE TABLE [TmpSysMenu] (
	[MenuId] INT NOT NULL, 
	[SiteId] INT NOT NULL,
    [MenuLevel] INT NOT NULL,
    [ParentId] INT NULL, 
    [MenuName] NVARCHAR(100) NOT NULL, 
    [MenuIcon] NVARCHAR(500) NULL, 
    [MenuOrder] INT NOT NULL, 
    [MvcArea] NVARCHAR(100) NOT NULL, 
    [MvcController] NVARCHAR(100) NOT NULL, 
    [MvcAction] NVARCHAR(100) NOT NULL, 
	[FlagActive] BIT NOT NULL DEFAULT 1,
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL
);
GO
-- Site 2 Main
INSERT INTO [TmpSysMenu] VALUES (2100, 2, 1, NULL, N'หน้าแรก', N'<i class="site-menu-icon wb-dashboard" aria-hidden="true"></i>', 1, N'', N'Home', N'Index', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2200, 2, 1, NULL, N'ตั้งค่าพื้นฐาน', N'<i class="site-menu-icon wb-file" aria-hidden="true"></i>', 2, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2210, 2, 2, 2200, N'ข้อมูลพนักงาน', N'', 1, N'', N'', N'', 1, 1, GETDATE());
		INSERT INTO [TmpSysMenu] VALUES (2211, 2, 3, 2210, N'พนักงาน', N'', 1, N'', N'Employee', N'Index', 1, 1, GETDATE());
		INSERT INTO [TmpSysMenu] VALUES (2212, 2, 3, 2210, N'ทีมขาย', N'', 2, N'', N'TeamSale', N'Index', 1, 1, GETDATE());
		INSERT INTO [TmpSysMenu] VALUES (2213, 2, 3, 2210, N'ทีมช่าง', N'', 3, N'', N'TeamOperation', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2220, 2, 2, 2200, N'แผนก', N'', 2, N'', N'Department', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2270, 2, 2, 2200, N'ตำแหน่ง', N'', 3, N'', N'Position', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2230, 2, 2, 2200, N'สินค้า', N'', 4, N'', N'Product', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2240, 2, 2, 2200, N'รุ่นสินค้า', N'', 5, N'', N'ProductModel', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2250, 2, 2, 2200, N'หมวดหมู่สินค้า', N'', 6, N'', N'ProductCategory', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2280, 2, 2, 2200, N'ประเภทสินค้า', N'', 7, N'', N'ProductType', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2260, 2, 2, 2200, N'หน่วยนับ', N'', 8, N'', N'Unit', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2310, 2, 2, 2200, N'ข้อมูลลูกค้า', N'', 9, N'', N'Customer', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2320, 2, 2, 2200, N'กลุ่มลูกค้า', N'', 10, N'', N'CustomerGroup', N'Index', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2300, 2, 1, NULL, N'ฝ่ายขาย', N'<i class="site-menu-icon wb-bookmark" aria-hidden="true"></i>', 3, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2340, 2, 2, 2300, N'ใบเสนอราคา', N'', 1, N'', N'Quotation', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2330, 2, 2, 2300, N'ใบสั่งขาย', N'', 2, N'', N'SaleOrder', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2350, 2, 2, 2300, N'ใบแจ้งหนี้', N'', 3 , N'', N'Invoice', N'Index', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2400, 2, 1, NULL, N'ฝ่ายติดตั้ง', N'<i class="site-menu-icon wb-hammer" aria-hidden="true"></i>', 4, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2410, 2, 2, 2400, N'ปฏิทินงาน', N'', 1, N'', N'Calendar', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2420, 2, 2, 2400, N'ใบสั่งงาน', N'', 2, N'', N'JobOrder', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2430, 2, 2, 2400, N'ใบนำวัสดุออก', N'', 3, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2440, 2, 2, 2400, N'ใบนำวัสดุเข้า', N'', 4, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2450, 2, 2, 2400, N'ตรวจสอบตำแหน่ง', N'', 5, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2460, 2, 2, 2400, N'ใบส่งงาน', N'', 6, N'', N'', N'', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2500, 2, 1, NULL, N'คลังสินค้า', N'<i class="site-menu-icon wb-plugin" aria-hidden="true"></i>', 5, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2510, 2, 2, 2500, N'สินค้าเข้า', N'', 1, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2520, 2, 2, 2500, N'สินค้าออก', N'', 2, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2530, 2, 2, 2500, N'รายการสินค้า', N'', 3, N'', N'', N'', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2600, 2, 1, NULL, N'ระบบการเงิน', N'<i class="site-menu-icon wb-extension" aria-hidden="true"></i>', 6, N'', N'', N'', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2700, 2, 1, NULL, N'ตารางการทำงาน', N'<i class="site-menu-icon wb-table" aria-hidden="true"></i>', 7, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2710, 2, 2, 2700, N'ปฏิทิน', N'', 1, N'', N'Calendar', N'View', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2720, 2, 2, 2700, N'รายการงาน', N'', 2, N'', N'', N'', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2800, 2, 1, NULL, N'รายงาน', N'<i class="site-menu-icon wb-pie-chart" aria-hidden="true"></i>', 8, N'', N'', N'', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (2900, 2, 1, NULL, N'ข้อมูลระบบ', N'<i class="site-menu-icon wb-grid-4" aria-hidden="true"></i>', 9, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2910, 2, 2, 2900, N'ผู้ใช้งาน', N'', 1, N'', N'Account', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (2920, 2, 2, 2900, N'สิทธิ์การใช้งาน', N'', 2, N'', N'Role', N'Index', 1, 1, GETDATE());
-- Site 3 Calculator Apps
INSERT INTO [TmpSysMenu] VALUES (3100, 3, 1, NULL, N'หน้าแรก', N'<i class="fa fa-dashboard"></i>', 1, N'', N'Home', N'Index', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (3200, 3, 1, NULL, N'การคำนวณ', N'<i class="fa fa-book"></i>', 2, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3210, 3, 2, 3200, N'Load Calculation', N'<i class="fa fa-tasks"></i>', 1, N'', N'Soil', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3220, 3, 2, 3200, N'Kunzelstab Calculation', N'<i class="fa fa-tasks"></i>', 3, N'', N'KPT', N'Index', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (3700, 3, 1, NULL, N'รายงาน', N'<i class="fa fa-chart"></i>', 7, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3710, 3, 2, 3700, N'Load Calculation', N'<i class="fa fa-tasks"></i>', 1, N'', N'Report', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3720, 3, 2, 3700, N'Kunzelstab Calculation', N'<i class="fa fa-tasks"></i>', 2, N'', N'Report', N'Maps', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (3800, 3, 1, NULL, N'ข้อมูลเข็ม', N'<i class="fa fa-globe"></i>', 8, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3810, 3, 2, 3800, N'หมวดหมู่', N'<i class="fa fa-user"></i>', 1, N'', N'PileSeries', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3820, 3, 2, 3800, N'เข็ม', N'<i class="fa fa-user-secret"></i>', 2, N'', N'Pile', N'Index', 1, 1, GETDATE());
INSERT INTO [TmpSysMenu] VALUES (3900, 3, 1, NULL, N'ข้อมูลระบบ', N'<i class="fa fa-globe"></i>', 9, N'', N'', N'', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3910, 3, 2, 3900, N'ผู้ใช้งาน', N'<i class="fa fa-user"></i>', 1, N'', N'Account', N'Index', 1, 1, GETDATE());
	INSERT INTO [TmpSysMenu] VALUES (3920, 3, 2, 3900, N'สิทธิ์การใช้งาน', N'<i class="fa fa-user-secret"></i>', 2, N'', N'Role', N'Index', 1, 1, GETDATE());
GO
MERGE INTO [SysMenu] AS trg USING (
	SELECT
		[MenuId],
		[SiteId],
		[MenuLevel],
		[ParentId],
		[MenuName],
		[MenuIcon],
		[MenuOrder],
		[MvcArea],
		[MvcController],
		[MvcAction],
		[FlagActive],
		[UpdatedBy],
		[UpdatedDate]
	FROM [TmpSysMenu]) AS src
ON trg.[MenuId] = src.[MenuId]
WHEN NOT MATCHED THEN
	INSERT (
		[MenuId],
		[SiteId],
		[MenuLevel],
		[ParentId],
		[MenuName],
		[MenuIcon],
		[MenuOrder],
		[MvcArea],
		[MvcController],
		[MvcAction],
		[FlagActive],
		[UpdatedBy],
		[UpdatedDate]
	) VALUES (
		src.[MenuId],
		src.[SiteId],
		src.[MenuLevel],
		src.[ParentId],
		src.[MenuName],
		src.[MenuIcon],
		src.[MenuOrder],
		src.[MvcArea],
		src.[MvcController],
		src.[MvcAction],
		src.[FlagActive],
		src.[UpdatedBy],
		src.[UpdatedDate])
WHEN MATCHED THEN
	UPDATE SET
		trg.[SiteId] = src.[SiteId],
		trg.[MenuLevel] = src.[MenuLevel],
		trg.[ParentId] = src.[ParentId],
		trg.[MenuName] = src.[MenuName],
		trg.[MenuIcon] = src.[MenuIcon],
		trg.[MenuOrder] = src.[MenuOrder],
		trg.[MvcArea] = src.[MvcArea],
		trg.[MvcController] = src.[MvcController],
		trg.[MvcAction] = src.[MvcAction],
		trg.[FlagActive] = src.[FlagActive];
GO
DROP TABLE [TmpSysMenu];
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpSysMenuPermission')
BEGIN
	DROP TABLE [TmpSysMenuPermission];
END
GO
CREATE TABLE [TmpSysMenuPermission] (
	[MenuId] INT NOT NULL , 
    [PermissionId] INT NOT NULL
);
GO
-- Site 2 Main
INSERT INTO [TmpSysMenuPermission] VALUES (2100, 1); --N'หน้าแรก'
INSERT INTO [TmpSysMenuPermission] VALUES (2200, 1); --N'ตั้งค่าพื้นฐาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2210, 1); --N'ข้อมูลพนักงาน'
		INSERT INTO [TmpSysMenuPermission] VALUES (2211, 1); --N'พนักงาน'
		INSERT INTO [TmpSysMenuPermission] VALUES (2211, 2);
		INSERT INTO [TmpSysMenuPermission] VALUES (2211, 3);
		INSERT INTO [TmpSysMenuPermission] VALUES (2212, 1); --N'ทีมขาย'
		INSERT INTO [TmpSysMenuPermission] VALUES (2212, 2);
		INSERT INTO [TmpSysMenuPermission] VALUES (2212, 3);
		INSERT INTO [TmpSysMenuPermission] VALUES (2213, 1); --N'ทีมช่าง'
		INSERT INTO [TmpSysMenuPermission] VALUES (2213, 2);
		INSERT INTO [TmpSysMenuPermission] VALUES (2213, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2220, 1); --N'แผนก'
	INSERT INTO [TmpSysMenuPermission] VALUES (2220, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2220, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2270, 1); --N'ตำแหน่ง'
	INSERT INTO [TmpSysMenuPermission] VALUES (2270, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2270, 3)
	INSERT INTO [TmpSysMenuPermission] VALUES (2230, 1); --N'สินค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2230, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2230, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2240, 1); --N'รุ่นสินค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2240, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2240, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2250, 1); --N'หมวดหมู่สินค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2250, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2250, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2280, 1); --N'ประเภทสินค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2280, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2280, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2260, 1); --N'หน่วยนับ'
	INSERT INTO [TmpSysMenuPermission] VALUES (2260, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2260, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2310, 1); --N'ข้อมูลลูกค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2310, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2310, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2320, 1); --N'กลุ่มลูกค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2320, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2320, 3);
INSERT INTO [TmpSysMenuPermission] VALUES (2300, 1); --N'ฝ่ายขาย'
	INSERT INTO [TmpSysMenuPermission] VALUES (2330, 1); --N'ใบสั่งขาย'
	INSERT INTO [TmpSysMenuPermission] VALUES (2330, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2330, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2340, 1); --N'ใบเสนอราคา'
	INSERT INTO [TmpSysMenuPermission] VALUES (2340, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2340, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2350, 1); --N'ใบแจ้งหนี้'
	INSERT INTO [TmpSysMenuPermission] VALUES (2350, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2350, 3);
INSERT INTO [TmpSysMenuPermission] VALUES (2400, 1); --N'ฝ่ายติดตั้ง'
	INSERT INTO [TmpSysMenuPermission] VALUES (2410, 1); --N'ปฏิทินงาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2410, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2410, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2420, 1); --N'ใบสั่งงาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2420, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2420, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2430, 1); --N'ใบนำวัสดุออก'
	INSERT INTO [TmpSysMenuPermission] VALUES (2430, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2430, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2440, 1); --N'ใบนำวัสดุเข้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2440, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2440, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2450, 1); --N'ตรวจสอบตำแหน่ง'
	INSERT INTO [TmpSysMenuPermission] VALUES (2450, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2450, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2460, 1); --N'ใบส่งงาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2460, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2460, 3);
INSERT INTO [TmpSysMenuPermission] VALUES (2500, 1); --N'คลังสินค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2510, 1); --N'สินค้าเข้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2510, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2510, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2520, 1); --N'สินค้าออก'
	INSERT INTO [TmpSysMenuPermission] VALUES (2520, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2520, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2530, 1); --N'รายการสินค้า'
	INSERT INTO [TmpSysMenuPermission] VALUES (2530, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2530, 3);
INSERT INTO [TmpSysMenuPermission] VALUES (2600, 1); --N'ระบบการเงิน'
INSERT INTO [TmpSysMenuPermission] VALUES (2700, 1); --N'ตารางการทำงาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2710, 1); --N'ปฏิทิน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2710, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2710, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2720, 1); --N'รายการงาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2720, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2720, 3);
INSERT INTO [TmpSysMenuPermission] VALUES (2800, 1); --N'รายงาน'
INSERT INTO [TmpSysMenuPermission] VALUES (2900, 1); --N'ข้อมูลระบบ'
	INSERT INTO [TmpSysMenuPermission] VALUES (2910, 1); --N'ผู้ใช้งาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2910, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2910, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (2920, 1); --N'สิทธิ์การใช้งาน'
	INSERT INTO [TmpSysMenuPermission] VALUES (2920, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (2920, 3);
-- Site 3 Calculator Apps
INSERT INTO [TmpSysMenuPermission] VALUES (3100, 1); --N'หน้าแรก', N'<i class="fa fa-dashboard"></i>', 1, N'Backend', N'Home', N'Index');
INSERT INTO [TmpSysMenuPermission] VALUES (3200, 1); --N'การคำนวณ', N'<i class="fa fa-book"></i>', 2, N'', N'', N'');
	INSERT INTO [TmpSysMenuPermission] VALUES (3210, 1); --N'Load Calc', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Soil', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3210, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (3210, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (3210, 4);
	INSERT INTO [TmpSysMenuPermission] VALUES (3220, 1); --N'KPT', N'<i class="fa fa-tasks"></i>', 3, N'Backend', N'KPT', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3220, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (3220, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (3220, 4);
INSERT INTO [TmpSysMenuPermission] VALUES (3700, 1); --N'รายงาน', N'<i class="fa fa-sitemap"></i>', 3, N'', N'', N'');
	INSERT INTO [TmpSysMenuPermission] VALUES (3710, 1); --N'ตัวอย่าง', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Account', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3720, 1); --N'ตัวอย่าง', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Account', N'Index');
INSERT INTO [TmpSysMenuPermission] VALUES (3800, 1); --N'ข้อมูลเข็ม', N'<i class="fa fa-sitemap"></i>', 3, N'', N'', N'');
	INSERT INTO [TmpSysMenuPermission] VALUES (3810, 1); --N'หมวดหมู่', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Account', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3810, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (3810, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (3820, 1); --N'เข็ม', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Role', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3820, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (3820, 3);
INSERT INTO [TmpSysMenuPermission] VALUES (3900, 1); --N'ข้อมูลระบบ', N'<i class="fa fa-sitemap"></i>', 3, N'', N'', N'');
	INSERT INTO [TmpSysMenuPermission] VALUES (3910, 1); --N'ผู้ใช้งาน', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Account', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3910, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (3910, 3);
	INSERT INTO [TmpSysMenuPermission] VALUES (3920, 1); --N'สิทธิ์การใช้งาน', N'<i class="fa fa-tasks"></i>', 1, N'Backend', N'Role', N'Index');
	INSERT INTO [TmpSysMenuPermission] VALUES (3920, 2);
	INSERT INTO [TmpSysMenuPermission] VALUES (3920, 3);
GO
MERGE INTO [SysMenuPermission] AS trg USING (
	SELECT
		[MenuId],
		[PermissionId]
	FROM [TmpSysMenuPermission]) AS src
ON
	trg.[MenuId] = src.[MenuId]
	AND trg.[PermissionId] = src.[PermissionId]
WHEN NOT MATCHED THEN
	INSERT (
		[MenuId],
		[PermissionId]
	) VALUES (
		src.[MenuId],
		src.[PermissionId]);
GO
DROP TABLE [TmpSysMenuPermission];
GO
-- SysMenuActive 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TmpSysMenuActive')
BEGIN
	DROP TABLE [TmpSysMenuActive];
END
GO
CREATE TABLE [TmpSysMenuActive] (
	[Id] INT NOT NULL,
    [MenuId] INT NOT NULL, 
    [MvcArea] NVARCHAR(100) NOT NULL, 
    [MvcController] NVARCHAR(100) NOT NULL, 
    [MvcAction] NVARCHAR(100) NOT NULL
);
GO
-- Site 2 Main
INSERT INTO [TmpSysMenuActive] VALUES (210001, 2100, N'', N'Home', N'Index');				--N'หน้าแรก'
INSERT INTO [TmpSysMenuActive] VALUES (210002, 2100, N'', N'Profile', N'Index');				-- ข้อมูลส่วนตัว
INSERT INTO [TmpSysMenuActive] VALUES (210003, 2100, N'', N'Profile', N'ChangePassword');		-- เปลี่ยนรหัสผ่าน
--INSERT INTO [TmpSysMenuActive] VALUES (220001, 2200, N'', N'', N''); --N'ตั้งค่าพื้นฐาน'
	INSERT INTO [TmpSysMenuActive] VALUES (221001, 2210, N'', N'Employee', N'Index'); --N'ข้อมูลพนักงาน'
	INSERT INTO [TmpSysMenuActive] VALUES (221002, 2210, N'', N'Employee', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (222001, 2220, N'', N'Department', N'Index'); --N'แผนก'
	INSERT INTO [TmpSysMenuActive] VALUES (222002, 2220, N'', N'Department', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (227001, 2270, N'', N'Position', N'Index'); --N'ตำแหน่ง'
	INSERT INTO [TmpSysMenuActive] VALUES (227002, 2270, N'', N'Position', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (223001, 2230, N'', N'Product', N'Index'); --N'สินค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (223002, 2230, N'', N'Product', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (224001, 2240, N'', N'ProductModel', N'Index'); --N'รุ่นสินค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (224002, 2240, N'', N'ProductModel', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (225001, 2250, N'', N'ProductCategory', N'Index'); --N'หมวดหมู่สินค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (225002, 2250, N'', N'ProductCategory', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (228001, 2280, N'', N'ProductType', N'Index'); --N'ประเภทสินค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (228002, 2280, N'', N'ProductType', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (226001, 2260, N'', N'Unit', N'Index'); --N'หน่วยนับ'
	INSERT INTO [TmpSysMenuActive] VALUES (226002, 2260, N'', N'Unit', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (231001, 2310, N'', N'Customer', N'Index'); --N'ข้อมูลลูกค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (231002, 2310, N'', N'Customer', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (232001, 2320, N'', N'CustomerGroup', N'Index'); --N'กลุ่มลูกค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (232002, 2320, N'', N'CustomerGroup', N'Detail');
--INSERT INTO [TmpSysMenuActive] VALUES (230001, 2300, N'', N'', N''); --N'ฝ่ายขาย'
	INSERT INTO [TmpSysMenuActive] VALUES (233001, 2330, N'', N'SaleOrder', N'Index'); --N'ประเภทสินค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (233002, 2330, N'', N'SaleOrder', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (234001, 2340, N'', N'Quotation', N'Index'); --N'หน่วยนับ'
	INSERT INTO [TmpSysMenuActive] VALUES (234002, 2340, N'', N'Quotation', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (235001, 2350, N'', N'Invoice', N'Index'); --N'ข้อมูลลูกค้า'
	INSERT INTO [TmpSysMenuActive] VALUES (235002, 2350, N'', N'Invoice', N'Detail');
--INSERT INTO [TmpSysMenuActive] VALUES (240001, 2400, N'', N'', N''); --N'ฝ่ายติดตั้ง'
	INSERT INTO [TmpSysMenuActive] VALUES (241001, 2410, N'', N'Calendar', N'Index'); --N'ปฏิทินงาน'
	INSERT INTO [TmpSysMenuActive] VALUES (241002, 2410, N'', N'Calendar', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (242001, 2420, N'', N'JobOrder', N'Index'); --N'ปฏิทินงาน'
	INSERT INTO [TmpSysMenuActive] VALUES (242002, 2420, N'', N'JobOrder', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (243001, 2430, N'', N'XXX', N'Index'); --N'ใบนำวัสดุออก'
	INSERT INTO [TmpSysMenuActive] VALUES (243002, 2430, N'', N'XXX', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (244001, 2440, N'', N'XXX', N'Index'); --N'ใบนำวัสดุเข้า'
	INSERT INTO [TmpSysMenuActive] VALUES (244002, 2440, N'', N'XXX', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (245001, 2450, N'', N'XXX', N'Index'); --N'ตรวจสอบตำแหน่ง'
	INSERT INTO [TmpSysMenuActive] VALUES (245002, 2450, N'', N'XXX', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (246001, 2460, N'', N'XXX', N'Index'); --N'ใบส่งงาน'
	INSERT INTO [TmpSysMenuActive] VALUES (246002, 2460, N'', N'XXX', N'Detail');
--INSERT INTO [TmpSysMenuActive] VALUES (290001, 2900, N'', N'', N''); --N'ข้อมูลระบบ'
	INSERT INTO [TmpSysMenuActive] VALUES (291001, 2910, N'', N'Staff', N'Index'); --N'ผู้ใช้งาน'
	INSERT INTO [TmpSysMenuActive] VALUES (291002, 2910, N'', N'Staff', N'Detail');
	INSERT INTO [TmpSysMenuActive] VALUES (292001, 2920, N'', N'Role', N'Index'); --N'สิทธิ์การใช้งาน'
	INSERT INTO [TmpSysMenuActive] VALUES (292002, 2920, N'', N'Role', N'Detail');
-- Site 3 Calculator Apps
GO
MERGE INTO [SysMenuActive] AS trg USING (
	SELECT
		[Id],
		[MenuId],
		[MvcArea],
		[MvcController],
		[MvcAction]
	FROM [TmpSysMenuActive]) AS src
ON trg.[Id] = src.[Id]
WHEN NOT MATCHED THEN
	INSERT (
		[Id],
		[MenuId],
		[MvcArea],
		[MvcController],
		[MvcAction]
	) VALUES (
		src.[Id],
		src.[MenuId],
		src.[MvcArea],
		src.[MvcController],
		src.[MvcAction])
WHEN MATCHED THEN
	UPDATE SET
		trg.[MenuId] = src.[MenuId],
		trg.[MvcArea] = src.[MvcArea],
		trg.[MvcController] = src.[MvcController],
		trg.[MvcAction] = src.[MvcAction];
GO
DROP TABLE [TmpSysMenuActive];
GO
SET IDENTITY_INSERT SysAccount ON;
GO
IF NOT EXISTS(SELECT * FROM SysAccount WHERE AccountId = 1)
BEGIN
	INSERT INTO SysAccount (AccountId, AccountUsername, AccountPassword, AccountFirstName, AccountLastName, AccountEmail, FlagStatus, FlagSystem, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
	VALUES (1, N'admin', N'AKfiU+aHecraaQnr81YxJktpItB54LFAGX+X17QR0vlv1ZNzZemwWqRYa97cTktxmg==', N'ศุภวัฒน์', N'ตันมณี', N'supawat.t@outlook.com', 1, 1, 1, GETDATE(), 1, GETDATE());
END
GO
SET IDENTITY_INSERT SysAccount OFF;
GO
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
