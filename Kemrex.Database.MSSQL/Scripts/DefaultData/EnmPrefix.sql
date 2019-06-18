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