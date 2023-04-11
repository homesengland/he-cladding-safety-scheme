






CREATE PROCEDURE [dbo].[GetAppraisalSurveyDetails]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
SELECT
	[ApplicationDetails].[Id] AS ApplicationId,
	[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[FireRiskAssessorListId] AS [FireRiskAssessorId],
	[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[DateOfInstruction],
	[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[SurveyDate]
FROM
	[ApplicationDetails]
	LEFT JOIN [ApplicationFireRiskAssessment] ON [ApplicationFireRiskAssessment].[Id] = [ApplicationDetails].[FireRiskAssessmentId]
	LEFT JOIN [ApplicationFireRiskAssessmentAppraisalSurveyDetails] ON [ApplicationFireRiskAssessmentAppraisalSurveyDetails].[FireRiskAssessmentId] = [ApplicationFireRiskAssessment].[Id]
WHERE
	[ApplicationDetails].[Id] = @ApplicationId
END