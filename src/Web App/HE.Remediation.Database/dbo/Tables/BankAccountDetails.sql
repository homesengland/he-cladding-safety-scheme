CREATE TABLE [dbo].[BankAccountDetails] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [NameOnTheAccount] NVARCHAR (150)   NOT NULL,
    [BankName]         NVARCHAR (150)   NOT NULL,
    [BranchName]       NVARCHAR (150)   NOT NULL,
    [AccountNumber]    NVARCHAR (150)   NOT NULL,
    [SortCode]         NVARCHAR (150)   NOT NULL,
    CONSTRAINT [PK_BankAccountDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

