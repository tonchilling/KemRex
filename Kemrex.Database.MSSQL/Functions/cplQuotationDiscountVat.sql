CREATE FUNCTION [dbo].[cplQuotationDiscountVat]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(DiscountVat) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
