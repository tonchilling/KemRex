CREATE TABLE [dbo].[SysDistrict]
(
	[DistrictId] INT IDENTITY(1,1) NOT NULL,
	[DistrictCode] NVARCHAR(4) NULL,
	[DistrictNameTH] NVARCHAR(256) NULL,
	[DistrictNameEN] NVARCHAR(256) NULL,
	[StateId] INT NULL, 
    CONSTRAINT [PK_SysDistrict] PRIMARY KEY ([DistrictId]), 
    CONSTRAINT [FK_SysDistrict_Province] FOREIGN KEY ([StateId]) REFERENCES [SysState]([StateId])
		ON UPDATE CASCADE
		ON DELETE SET NULL
)
