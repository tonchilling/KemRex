CREATE FUNCTION [dbo].[cplQuotationSummaryVat]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(PriceVat - DiscountVat) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
