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