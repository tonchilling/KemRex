CREATE TABLE [dbo].[TblCustomer]
(
	[CustomerId] INT NOT NULL IDENTITY(1, 1), 
    [PrefixId] INT NULL, 
    [CustomerName] NVARCHAR(500) NOT NULL, 
    [CustomerAvatar] NVARCHAR(500) NULL, 
    [CustomerTaxId] NVARCHAR(20) NULL, 
    [CustomerPhone] NVARCHAR(50) NULL, 
    [CustomerFax] NVARCHAR(50) NULL, 
    [CustomerEmail] NVARCHAR(200) NULL, 
    [CustomerType] INT NOT NULL, 
    [GroupId] INT NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblCustomer] PRIMARY KEY ([CustomerId]), 
    CONSTRAINT [FK_TblCustomer_Prefix] FOREIGN KEY ([PrefixId]) REFERENCES [EnmPrefix]([PrefixId]), 
    CONSTRAINT [FK_TblCustomer_Group] FOREIGN KEY ([GroupId]) REFERENCES [TblCustomerGroup]([GroupId]) 
)
