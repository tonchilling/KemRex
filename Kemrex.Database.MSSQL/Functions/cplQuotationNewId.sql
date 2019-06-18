CREATE FUNCTION [dbo].[cplQuotationNewId] ()
RETURNS VARCHAR(20)
AS
BEGIN
	
	DECLARE @oid varchar(20);
	DECLARE @pre varchar(10);
	DECLARE @dnow varchar(10);
	DECLARE @runid int;
	set @dnow='QU' + (select FORMAT(GETDATE(),'ddMMyy','th-TH'));
	set @oid = (Select MAX(QuotationNo) from TblQuotation);
	set @pre=isnull(LEFT(@oid,8),@dnow);
	set @runid=isnull(TRY_CONVERT(int,RIGHT(@oid ,3)),1);

	if (@oid=@dnow)
		set @runid=@runid+1;
	else
		set @runid=1;

	RETURN @dnow + '-' + FORMAT(@runid,'000');

END