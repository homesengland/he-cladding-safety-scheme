CREATE TABLE [dbo].[ApplicationFundingRoutes] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [AlternateFundingId]  UNIQUEIDENTIFIER NOT NULL,
    [FundingRoutesTypeId] INT              NOT NULL,
    CONSTRAINT [FK_ApplicationFundingRoutes_ApplicationFundingRoutesType] FOREIGN KEY ([FundingRoutesTypeId]) REFERENCES [dbo].[ApplicationFundingRoutesType] ([Id])
);






