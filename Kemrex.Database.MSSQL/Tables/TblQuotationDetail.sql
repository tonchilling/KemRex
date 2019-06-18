CREATE TABLE [dbo].[TblQuotationDetail]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [QuotationId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [PriceNet] DECIMAL(12, 2) NOT NULL, 
    [PriceVat] DECIMAL(12, 2) NOT NULL, 
    [PriceTot] DECIMAL(12, 2) NOT NULL, 
    [DiscountNet] DECIMAL(12, 2) NOT NULL, 
    [DiscountVat] DECIMAL(12, 2) NOT NULL, 
    [DiscountTot] DECIMAL(12, 2) NOT NULL, 
    [TotalNet] AS (PriceNet - DiscountNet), 
    [TotalVat] AS (PriceVat - DiscountVat), 
    [TotalTot] AS (PriceTot - DiscountTot), 
    [Remark] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_TblQuotationDetail] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TblQuotationDetail_id] FOREIGN KEY ([QuotationId]) REFERENCES [TblQuotation]([QuotationId]), 
    CONSTRAINT [FK_TblQuotationDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [TblProduct]([ProductId])
)
