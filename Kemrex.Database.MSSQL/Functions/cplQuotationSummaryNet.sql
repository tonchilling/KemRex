CREATE FUNCTION [dbo].[cplQuotationSummaryNet]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(PriceNet - DiscountNet) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
