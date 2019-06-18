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