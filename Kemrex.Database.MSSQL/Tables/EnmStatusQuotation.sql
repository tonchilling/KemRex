CREATE TABLE [dbo].[EnmStatusQuotation]
(
	[StatusId] INT NOT NULL, 
    [StatusName] NVARCHAR(100) NOT NULL, 
    [StatusOrder] INT NOT NULL, 
    CONSTRAINT [PK_EnmStatusQuotation] PRIMARY KEY ([StatusId]) 
)
