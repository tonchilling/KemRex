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