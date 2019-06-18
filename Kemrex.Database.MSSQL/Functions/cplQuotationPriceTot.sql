CREATE FUNCTION [dbo].[cplQuotationPriceTot]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(PriceTot) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
