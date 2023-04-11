CREATE TABLE [dbo].[ApplicationFireRiskAssessmentAppraisalSurveyDetails] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [FireRiskAssessmentId]   UNIQUEIDENTIFIER NOT NULL,
    [FireRiskAssessorListId] INT              NULL,
    [DateOfInstruction]      DATETIME2 (7)    NOT NULL,
    [SurveyDate]             DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ApplicationFireRiskAssessmentAppraisalServiceDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationFireRiskAssessmentAppraisalServiceDetails_ApplicationFireRiskAssessment] FOREIGN KEY ([FireRiskAssessmentId]) REFERENCES [dbo].[ApplicationFireRiskAssessment] ([Id]),
    CONSTRAINT [FK_ApplicationFireRiskAssessmentAppraisalServiceDetails_FireRiskAssessorList] FOREIGN KEY ([FireRiskAssessorListId]) REFERENCES [dbo].[FireRiskAssessorList] ([Id])
);



