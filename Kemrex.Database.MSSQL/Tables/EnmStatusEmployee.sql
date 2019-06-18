CREATE TABLE [dbo].[EnmStatusEmployee]
(
	[StatusId] INT NOT NULL, 
    [StatusName] NVARCHAR(100) NOT NULL, 
    [StatusOrder] INT NOT NULL, 
    CONSTRAINT [PK_EnmStatusEmployee] PRIMARY KEY ([StatusId]) 
)
