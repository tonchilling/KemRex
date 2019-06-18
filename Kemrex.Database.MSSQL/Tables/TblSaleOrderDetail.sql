CREATE TABLE [dbo].[TblSaleOrderDetail]
(
	[Id] INT  NOT NULL IDENTITY(1, 1), 
    [SaleOrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [PriceNet] DECIMAL(12, 2) NOT NULL, 
    [PriceVat] DECIMAL(12, 2) NOT NULL, 
    [PriceTot] DECIMAL(12, 2) NOT NULL, 
    [DiscountNet] DECIMAL(12, 2) NOT NULL, 
    [DiscountVat] DECIMAL(12, 2) NOT NULL, 
    [DiscountTot] DECIMAL(12, 2) NOT NULL, 
    [TotalNet] AS (PriceNet - DiscountNet), 
    [TotalVat]  AS (PriceVat - DiscountVat),
    [TotalTot] AS (PriceTot - DiscountTot), 
    [Remark] NCHAR(10) NULL,
	CONSTRAINT [PK_TblSaleOrderDetail] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TblSaleOrderDetail_id] FOREIGN KEY ([SaleOrderId]) REFERENCES [TblSaleOrder]([SaleOrderId]), 
    CONSTRAINT [FK_TblSaleOrderDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [TblProduct]([ProductId])
)
