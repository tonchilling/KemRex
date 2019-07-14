CREATE TABLE [dbo].[TblInvoice]
(
	[InvoiceId] INT NOT NULL IDENTITY(1, 1), 
    [InvoiceNo] NVARCHAR(50) NOT NULL, 
    [InvoiceDate] DATETIME NOT NULL, 
    [SaleOrderId] INT NOT NULL, 
    [InvoiceRemark] NVARCHAR(MAX) NULL, 
    [InvoiceTerm] INT NOT NULL, 
    [InvoiceAmount] DECIMAL(12, 2) NULL, 
    [StatusId] INT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdateDate] DATETIME NOT NULL,
    [DueDate] DATETIME NULL, 
    CONSTRAINT [PK_TblInvoice] PRIMARY KEY ([InvoiceId]), 
    CONSTRAINT [FK_TblInvoice_SaleOrder] FOREIGN KEY ([SaleOrderId]) REFERENCES [TblSaleOrder]([SaleOrderId])
)
