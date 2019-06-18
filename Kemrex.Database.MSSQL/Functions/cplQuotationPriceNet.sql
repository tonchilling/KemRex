CREATE FUNCTION [dbo].[cplQuotationPriceNet]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(PriceNet) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
