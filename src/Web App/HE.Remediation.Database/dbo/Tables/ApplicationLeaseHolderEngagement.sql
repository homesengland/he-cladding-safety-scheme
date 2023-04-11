CREATE TABLE [dbo].[ApplicationLeaseHolderEngagement] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Acknowledged] BIT              NULL,
    [TaskStatusId] INT              CONSTRAINT [DF_ApplicationLeaseHolderEngagement_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [FK_dbo.ApplicationLeaseHolderEngagement_dbo.TaskStatus_TaskStatusID] FOREIGN KEY ([TaskStatusId]) REFERENCES [dbo].[TaskStatus] ([Id])
);


