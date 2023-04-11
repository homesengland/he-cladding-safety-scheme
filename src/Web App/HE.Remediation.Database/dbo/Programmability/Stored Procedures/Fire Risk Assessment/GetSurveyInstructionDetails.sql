




CREATE PROCEDURE [dbo].[GetSurveyInstructionDetails]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
SELECT
	[ApplicationDetails].[Id] AS ApplicationId,
	[ApplicationFireRiskAssessmentSurveyInstructionDetails].[FireRiskAssessorListId] AS [FireRiskAssessorId],
	[ApplicationFireRiskAssessmentSurveyInstructionDetails].[DateOfInstruction]
FROM
	[ApplicationDetails]
	LEFT JOIN [ApplicationFireRiskAssessment] ON [ApplicationFireRiskAssessment].[Id] = [ApplicationDetails].[FireRiskAssessmentId]
	LEFT JOIN [ApplicationFireRiskAssessmentSurveyInstructionDetails] ON [ApplicationFireRiskAssessmentSurveyInstructionDetails].[FireRiskAssessmentId] = [ApplicationFireRiskAssessment].[Id]
WHERE
	[ApplicationDetails].[Id] = @ApplicationId
END