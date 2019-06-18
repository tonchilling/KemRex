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