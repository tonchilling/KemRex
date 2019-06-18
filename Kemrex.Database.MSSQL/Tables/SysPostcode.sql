CREATE TABLE [dbo].[SysPostcode]
(
	[DistrictId] INT NOT NULL,
	[Postcode] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_SysPostcode] PRIMARY KEY ([DistrictId], [Postcode]), 
    CONSTRAINT [FK_SysPostcode_District] FOREIGN KEY ([DistrictId]) REFERENCES [SysDistrict]([DistrictId])
		ON UPDATE CASCADE
		ON DELETE CASCADE
)
