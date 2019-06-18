CREATE FUNCTION [dbo].[cplQuotationPriceVat]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(PriceVat) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
