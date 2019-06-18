CREATE TABLE [dbo].[TblCustomerAddress]
(
	[AddressId] INT NOT NULL IDENTITY(1, 1), 
	[CustomerId] INT NOT NULL,
    [AddressName] NVARCHAR(100) NOT NULL, 
    [AddressValue] NVARCHAR(500) NOT NULL, 
    [SubDistrictId] INT NULL, 
    [AddressPostcode] NVARCHAR(10) NULL, 
	[AddressContact] NVARCHAR(500) NULL,
	[AddressContactEmail] NVARCHAR(500) NULL,
	[AddressContactPhone] NVARCHAR(500) NULL,
    [AddressOrder] INT NOT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblCustomerAddress] PRIMARY KEY ([AddressId]), 
    CONSTRAINT [FK_TblCustomerAddress_Id] FOREIGN KEY ([CustomerId]) REFERENCES [TblCustomer]([CustomerId]), 
    CONSTRAINT [FK_TblCustomerAddress_SubDistrict] FOREIGN KEY ([SubDistrictId]) REFERENCES [SysSubDistrict]([SubDistrictId])
)
