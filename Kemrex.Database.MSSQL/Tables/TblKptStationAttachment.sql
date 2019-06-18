CREATE TABLE [dbo].[TblKptStationAttachment]
(
	[AttachId] INT NOT NULL IDENTITY(1, 1), 
    [StationId] INT NOT NULL, 
    [AttachName] NVARCHAR(200) NOT NULL, 
    [AttachPath] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_TblKptStationAttachment] PRIMARY KEY ([AttachId]), 
    CONSTRAINT [FK_TblKptStationAttachment_Id] FOREIGN KEY ([StationId]) REFERENCES [TblKptStation]([StationId])
)
