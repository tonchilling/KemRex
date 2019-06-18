CREATE FUNCTION [dbo].[cplSaleOrderPrice]
(
	@id int,
	@func varchar(50) 
)
RETURNS DECIMAL(12, 2)
AS
BEGIN
if (@func='PriceNet') 
	RETURN ISNULL((SELECT SUM(PriceNet) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='PriceTot') 
		RETURN ISNULL((SELECT SUM(PriceTot) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='PriceVat') 
		RETURN ISNULL((SELECT SUM(PriceVat) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='DiscountNet') 
		RETURN ISNULL((SELECT SUM(DiscountNet) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='DiscountTot') 
		RETURN ISNULL((SELECT SUM(DiscountTot) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='DiscountVat') 
		RETURN ISNULL((SELECT SUM(DiscountVat) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='SummaryNet') 
		RETURN ISNULL((SELECT SUM(PriceNet - DiscountNet) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='SummaryTot') 
		RETURN ISNULL((SELECT SUM(PriceTot - DiscountTot) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);
else if (@func='SummaryVat') 
		RETURN ISNULL((SELECT SUM(PriceVat - DiscountVat) FROM TblSaleOrderDetail WHERE SaleOrderId = @id), 0);

		RETURN 0;
END
