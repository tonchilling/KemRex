CREATE TABLE [dbo].[TblWareHouse](
	[WHId] [int] IDENTITY(1,1) NOT NULL,
	[WHCode] [nvarchar](100) NULL,
	[WHName] [nvarchar](100) NULL,
	[FlagActive] [int] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[Updatedby] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK__TblWareHouse] PRIMARY KEY CLUSTERED 
(
	[WHId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
