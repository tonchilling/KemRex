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