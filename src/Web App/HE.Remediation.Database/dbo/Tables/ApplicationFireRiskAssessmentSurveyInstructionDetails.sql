CREATE TABLE [dbo].[ApplicationFireRiskAssessmentSurveyInstructionDetails] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [FireRiskAssessmentId]   UNIQUEIDENTIFIER NOT NULL,
    [FireRiskAssessorListId] INT              NOT NULL,
    [DateOfInstruction]      DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ApplicationFireRiskAssessmentSurveyInstructionDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationFireRiskAssessmentSurveyInstructionDetails_ApplicationFireRiskAssessment] FOREIGN KEY ([FireRiskAssessmentId]) REFERENCES [dbo].[ApplicationFireRiskAssessment] ([Id]),
    CONSTRAINT [FK_ApplicationFireRiskAssessmentSurveyInstructionDetails_FireRiskAssessorList] FOREIGN KEY ([FireRiskAssessorListId]) REFERENCES [dbo].[FireRiskAssessorList] ([Id])
);

