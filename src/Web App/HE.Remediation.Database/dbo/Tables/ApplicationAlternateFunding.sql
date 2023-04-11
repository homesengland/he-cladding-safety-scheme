CREATE TABLE [dbo].[ApplicationAlternateFunding] (
    [Id]                        UNIQUEIDENTIFIER NOT NULL,
    [OtherSourcesPursuedTypeId] INT              NOT NULL,
    [TaskStatusId]              INT              NULL,
    CONSTRAINT [PK_ApplicationAlternateFunding] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationAlternateFunding_ApplicationOtherSourcesPursuedType] FOREIGN KEY ([OtherSourcesPursuedTypeId]) REFERENCES [dbo].[ApplicationOtherSourcesPursuedType] ([Id]),
    CONSTRAINT [FK_ApplicationAlternateFunding_TaskStatus] FOREIGN KEY ([TaskStatusId]) REFERENCES [dbo].[TaskStatus] ([Id])
);




