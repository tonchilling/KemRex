CREATE TABLE [dbo].[TblCustomerContact]
(
	[ContactId] INT NOT NULL IDENTITY(1, 1), 
    [CustomerId] INT NOT NULL, 
    [ContactName] NVARCHAR(200) NOT NULL, 
    [ContactPosition] NVARCHAR(200) NULL, 
    [ContactEmail] NVARCHAR(500) NULL, 
    [ContactPhone] NVARCHAR(50) NULL, 
    [CreatedBy] BIGINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] BIGINT NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_TblCustomerContact] PRIMARY KEY ([ContactId]), 
    CONSTRAINT [FK_TblCustomerContact_Id] FOREIGN KEY ([CustomerId]) REFERENCES [TblCustomer]([CustomerId])
)
