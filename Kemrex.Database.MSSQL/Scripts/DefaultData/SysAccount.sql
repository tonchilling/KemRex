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