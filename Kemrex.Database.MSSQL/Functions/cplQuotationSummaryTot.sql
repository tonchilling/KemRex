CREATE FUNCTION [dbo].[cplQuotationSummaryTot]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(PriceTot - DiscountTot) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
