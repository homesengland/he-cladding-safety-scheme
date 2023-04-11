CREATE TABLE [dbo].[TaskStatus] (
    [Id]   INT            NOT NULL,
    [Type] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_TaskStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

