CREATE FUNCTION [dbo].[cplQuotationDiscountTot]
(
	@quotationId INT
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
	RETURN ISNULL((SELECT SUM(DiscountTot) FROM TblQuotationDetail WHERE QuotationId = @quotationId), 0);
END
