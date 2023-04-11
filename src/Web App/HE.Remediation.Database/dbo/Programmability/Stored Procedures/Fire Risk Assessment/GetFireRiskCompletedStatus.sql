CREATE PROCEDURE [dbo].[GetFireRiskCompletedStatus]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
	TOP 1 [FireRiskCompleted] AS IsAppraisalCompleted
	FROM [ApplicationFireRiskAssessment] afra
	INNER JOIN [ApplicationDetails] ad
	ON afra.[Id] = ad.[FireRiskAssessmentId]
	WHERE ad.[Id] = @ApplicationId
END
