CREATE TABLE [dbo].[SysSite]
(
	[SiteId] INT NOT NULL IDENTITY(1, 1), 
    [SiteName] NVARCHAR(100) NOT NULL, 
    [SiteDetail] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_SysSite] PRIMARY KEY ([SiteId])
)
