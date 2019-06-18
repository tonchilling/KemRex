CREATE TABLE [dbo].[TblPileSeries]
(
	[SeriesId] INT IDENTITY (1, 1) NOT NULL,
    [SeriesName]  NVARCHAR (100) NOT NULL,
    [SeriesImage] NVARCHAR(500)  NOT NULL, 
    [SeriesOrder] INT            NOT NULL,
    CONSTRAINT [PK_TblPileSeries] PRIMARY KEY CLUSTERED ([SeriesId] ASC)
)
