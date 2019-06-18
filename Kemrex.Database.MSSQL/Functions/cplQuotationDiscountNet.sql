CREATE FUNCTION [dbo].[cplQuotationDiscountNet]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(DiscountNet) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
