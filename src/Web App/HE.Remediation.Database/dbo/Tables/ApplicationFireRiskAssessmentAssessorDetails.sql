CREATE TABLE [dbo].[ApplicationFireRiskAssessmentAssessorDetails] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [FireRiskAssessmentId] UNIQUEIDENTIFIER NOT NULL,
    [FirstName]            NVARCHAR (150)   NOT NULL,
    [LastName]             NVARCHAR (150)   NOT NULL,
    [CompanyName]          NVARCHAR (150)   NOT NULL,
    [CompanyNumber]        NVARCHAR (150)   NOT NULL,
    [EmailAddress]         NVARCHAR (150)   NOT NULL,
    [Telephone]            NVARCHAR (150)   NOT NULL,
    CONSTRAINT [PK_ApplicationFireRiskAssessmentAssessorDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationFireRiskAssessmentAssessorDetails_ApplicationFireRiskAssessment] FOREIGN KEY ([FireRiskAssessmentId]) REFERENCES [dbo].[ApplicationFireRiskAssessment] ([Id])
);

