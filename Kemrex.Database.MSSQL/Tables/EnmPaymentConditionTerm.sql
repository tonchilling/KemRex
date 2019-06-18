CREATE TABLE [dbo].[EnmPaymentConditionTerm]
(
    [ConditionId] INT NOT NULL, 
    [TermNo] INT NOT NULL, 
    [TermPercent] INT NOT NULL, 
    [TermAmount] INT NOT NULL, 
    [FlagLast] BIT NOT NULL, 
    CONSTRAINT [PK_EnmPaymentConditionTerm] PRIMARY KEY ([ConditionId], [TermNo]), 
    CONSTRAINT [FK_EnmPaymentConditionTerm_Id] FOREIGN KEY ([ConditionId]) REFERENCES [EnmPaymentCondition]([ConditionId])
)
