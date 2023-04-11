CREATE TABLE [dbo].[ApplicationBuildingDetails] (
    [Id]                            UNIQUEIDENTIFIER NOT NULL,
    [UniqueName]                    NVARCHAR (200) NULL,
    [ResidentialUnitsCount]         INT NULL,
    [NonResidentialUnits]           BIT NULL,
    [NonResidentialUnitsCount]      INT NULL,
    [AddressId]                     UNIQUEIDENTIFIER NULL,
    [PartOfDevelopment]             BIT NULL,
    [NameOfDevelopment]             NVARCHAR(150) NULL,
    [Storeys]                       INT NULL,
    [CorrectHeight]                 BIT NULL,
    [OriginalDeveloperKnown]        BIT NULL,
    [DeveloperId]                   UNIQUEIDENTIFIER NULL,
    [CorrectHeightConfirmedDate]    DATETIME NULL,
    [TaskStatusId]                  INT CONSTRAINT [DF_ApplicationBuildingDetails_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ApplicationBuildingDetails] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_dbo.ApplicationBuildingDetails_dbo.TaskStatus_TaskStatusID] FOREIGN KEY ([TaskStatusId]) REFERENCES [dbo].[TaskStatus] ([Id]),
    CONSTRAINT [FK_ApplicationBuildingDetails_DeveloperId] FOREIGN KEY ([DeveloperId]) REFERENCES [dbo].[ApplicationDeveloper] ([Id]),
    CONSTRAINT [FK_ApplicationBuildingDetails_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id])
);

