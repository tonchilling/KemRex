CREATE TABLE [dbo].[TblPile]
(
	[PileId] INT IDENTITY (1, 1) NOT NULL,
    [SeriesId]          INT            NULL,
    [PileName]         NVARCHAR (100) NOT NULL,
    [PileLength]       INT            NOT NULL,
    [PileDia]          DECIMAL (5, 4) NOT NULL,
    [PileBlade]        DECIMAL (5, 2) NOT NULL,
    [PileSpiralLength] DECIMAL (5, 4) NOT NULL,
    [PileSpiralDepth]  DECIMAL (5, 4) NOT NULL,
    [PileFlangeWidth]  DECIMAL (5, 2) NOT NULL,
    [PileFlangeLength] DECIMAL (5, 2) NOT NULL,
    [PilePrice] REAL NULL, 
    [PileSeriesOrder]  INT CONSTRAINT [DF_TblPile_PileSeriesOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TblPile] PRIMARY KEY CLUSTERED ([PileId] ASC),
    CONSTRAINT [FK_TblPile_Series] FOREIGN KEY ([SeriesId]) REFERENCES [dbo].[TblPileSeries] ([SeriesId])
)
