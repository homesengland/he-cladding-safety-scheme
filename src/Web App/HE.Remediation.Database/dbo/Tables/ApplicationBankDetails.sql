CREATE TABLE [dbo].[ApplicationBankDetails] (
    [Id]                                    UNIQUEIDENTIFIER NOT NULL,
    [ResponsibleEntityRelationship]         INT              NULL,
    [RepresentativeBankAccountDetailsId]    UNIQUEIDENTIFIER NULL,
    [ResponsibleEntityBankAccountDetailsId] UNIQUEIDENTIFIER NULL,
    [TaskStatusId]                          INT              CONSTRAINT [DF_ApplicationBankDetails_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ApplicationBankDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationBankDetails_BankAccountDetails_Representative] FOREIGN KEY ([RepresentativeBankAccountDetailsId]) REFERENCES [dbo].[BankAccountDetails] ([Id]),
    CONSTRAINT [FK_ApplicationBankDetails_BankAccountDetails_ResponsibleEntity] FOREIGN KEY ([ResponsibleEntityBankAccountDetailsId]) REFERENCES [dbo].[BankAccountDetails] ([Id]),
    CONSTRAINT [FK_dbo.ApplicationBankDetails_dbo.TaskStatus_TaskStatusID] FOREIGN KEY ([TaskStatusId]) REFERENCES [dbo].[TaskStatus] ([Id])
);







