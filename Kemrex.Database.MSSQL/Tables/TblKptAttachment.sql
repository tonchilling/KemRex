CREATE TABLE [dbo].[TblKptAttachment]
(
	[AttachId] INT NOT NULL IDENTITY(1, 1), 
    [KptId] INT NOT NULL, 
    [AttachName] NVARCHAR(200) NOT NULL, 
    [AttachPath] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_TblKptAttachment] PRIMARY KEY ([AttachId]), 
    CONSTRAINT [FK_TblKptAttachment_Id] FOREIGN KEY ([KptId]) REFERENCES [TblKpt]([KptId])
)
