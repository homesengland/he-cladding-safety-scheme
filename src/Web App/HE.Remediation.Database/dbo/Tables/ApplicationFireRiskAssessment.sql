CREATE TABLE [dbo].[ApplicationFireRiskAssessment] (
    [Id]                           UNIQUEIDENTIFIER NOT NULL,
    [FireRiskCompleted]            BIT              NULL,
    [SurveyInstructionDetailsId]   UNIQUEIDENTIFIER NULL,
    [AppraisalServiceDetailsId]    UNIQUEIDENTIFIER NULL,
    [FraewFileId]                  UNIQUEIDENTIFIER NULL,
    [AssessorDetailsId]            UNIQUEIDENTIFIER NULL,
    [TaskStatusId]                 INT              CONSTRAINT [DF_ApplicationFireRiskAssessment_Status] DEFAULT ((1)) NOT NULL,
    [AreaOrUnsafeCladdingReplaced] DECIMAL (19, 5)  NULL,
    [ReplacementTypeId]            UNIQUEIDENTIFIER NULL,
    [AreaOfUnsafeCladdingRemoved]  DECIMAL (19, 5)  NULL,
    CONSTRAINT [PK_ApplicationFireRiskAssessment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ApplicationFireRiskAssessment_dbo.TaskStatus_TaskStatusID] FOREIGN KEY ([TaskStatusId]) REFERENCES [dbo].[TaskStatus] ([Id])
);





