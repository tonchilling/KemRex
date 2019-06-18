CREATE TABLE [dbo].[TblSaleOrderAttachment]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [SaleOrderId] INT NOT NULL, 
    [AttachmentPath] NVARCHAR(500) NOT NULL, 
    [AttachmentOrder] INT NOT NULL DEFAULT 9999, 
    [AttachmentRemark] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_TblSaleOrderAttachment] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TblSaleOrderAttachment_Id] FOREIGN KEY ([SaleOrderId]) REFERENCES [TblSaleOrder]([SaleOrderId])
)
