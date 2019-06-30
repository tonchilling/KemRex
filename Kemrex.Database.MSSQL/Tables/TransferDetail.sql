CREATE TABLE [dbo].[TransferDetail](
	[TransferId] INT NOT NULL,
	[Seq] INT NOT NULL,
	[ProductId] [int] NULL,
	[CurrentQty] [varchar](50) NULL,
	[RequestQty] [int] NULL,
	[RequestUnit] [varchar](20) NULL,
	[RequestUnitFactor] [decimal](8, 3) NULL,
	[last_modified] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransferId] ASC,
	[Seq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TransferDetail]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[TblProduct] ([ProductId])