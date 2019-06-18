﻿CREATE TABLE [dbo].[SysGeography]
(
	[GeoId] INT IDENTITY(1,1) NOT NULL,
	[GeoNameTH] NVARCHAR(256) NULL,
	[GeoNameEN] NVARCHAR(256) NULL, 
    CONSTRAINT [PK_SysGeography] PRIMARY KEY ([GeoId])
)
