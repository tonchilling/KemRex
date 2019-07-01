CREATE TABLE [dbo].[TransferHeader](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[TransferNo] [nvarchar](15) NOT NULL,
	[TransferType] [nvarchar](3) NOT NULL,
	[TransferDate] [datetime] NULL,
	[TransferTime] [nvarchar](5) NULL,
	[ReceiveTo] [nvarchar](50) NULL,
	[Reason] [nvarchar](100) NULL,
	[CarType] [int] NULL,
	[Company] [nvarchar](100) NULL,
	[CarNo] [nchar](10) NULL,
	[CarBrand] [nchar](10) NULL,
	[SendToDepartment] [int] NULL,
	[Remark] [nvarchar](100) NULL,
	[EmpId] [nvarchar](10) NULL,
	[BillNo] [nvarchar](50) NULL,
	[TransferStatus] [int] NULL,
	[Note1] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK__Transfer__9548BE632F35F7F7] PRIMARY KEY CLUSTERED 
(
	[TransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]