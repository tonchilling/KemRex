CREATE TABLE [dbo].[SysSubDistrict]
(
	[SubDistrictId] INT IDENTITY(1,1) NOT NULL,
	[SubDistrictCode] NVARCHAR(6) NULL,
	[SubDistrictNameTH] NVARCHAR(256) NULL,
	[SubDistrictNameEN] NVARCHAR(256) NULL,
	[DistrictId] INT NULL, 
    [Postcode] NVARCHAR(10) NULL, 
    CONSTRAINT [PK_SysSubDistrict] PRIMARY KEY ([SubDistrictId]), 
    CONSTRAINT [FK_SysSubDistrict_District] FOREIGN KEY ([DistrictId]) REFERENCES [SysDistrict]([DistrictId])
		ON UPDATE CASCADE
		ON DELETE SET NULL
)
