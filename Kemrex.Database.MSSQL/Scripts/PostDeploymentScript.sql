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
:r .\DefaultData\EnmStatusEmployee.sql
:r .\DefaultData\EnmStatusQuotation.sql
:r .\DefaultData\EnmPrefix.sql
:r .\DefaultData\EnmPaymentCondition.sql
:r .\DefaultData\EnmPaymentConditionTerm.sql

:r .\DefaultData\SysSite.sql
:r .\DefaultData\SysParameter.sql
:r .\DefaultData\SysPermission.sql
:r .\DefaultData\SysMenu.sql
:r .\DefaultData\SysAccount.sql
:r .\DefaultData\SysRole.sql