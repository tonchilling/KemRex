CREATE TABLE [dbo].[EnmPaymentCondition]
(
	[ConditionId] INT NOT NULL, 
    [ConditionName] NVARCHAR(100) NOT NULL, 
    [ConditionTerm] INT NOT NULL DEFAULT 1, 
    [FlagActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_EnmPaymentCondition] PRIMARY KEY ([ConditionId]) 
)
