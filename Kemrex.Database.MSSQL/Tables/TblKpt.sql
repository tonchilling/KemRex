CREATE TABLE [dbo].[TblKpt]
(
	[KptId] INT NOT NULL IDENTITY(1, 1), 
    [ProjectName] NVARCHAR(200) NOT NULL, 
	[CustomerName] NVARCHAR(200) NULL,
	[KptLatitude] NVARCHAR(50) NULL,
	[KptLongtitude] NVARCHAR(50) NULL,
    [KptLocation] NVARCHAR(200) NULL, 
	[SubDistrictId] INT NULL,
    [KptStation] NVARCHAR(100) NULL, 
    [KptDate] DATETIME NOT NULL, 
    [TestBy] NVARCHAR(500) NULL, 
    [KptRemark] NVARCHAR(MAX) NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblKpt] PRIMARY KEY ([KptId]), 
    CONSTRAINT [FK_TblKpt_SubDistrict] FOREIGN KEY ([SubDistrictId]) REFERENCES [SysSubDistrict]([SubDistrictId]) 
)
