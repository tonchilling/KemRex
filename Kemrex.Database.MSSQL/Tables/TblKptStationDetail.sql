CREATE TABLE [dbo].[TblKptStationDetail]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1), 
    [StationId] INT NOT NULL, 
    [StationDepth] INT NOT NULL, 
    [StationBlowCount] INT NOT NULL, 
    CONSTRAINT [PK_TblKptStationDetail] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TblKptStationDetail_Id] FOREIGN KEY ([StationId]) REFERENCES [TblKptStation]([StationId]), 
)
