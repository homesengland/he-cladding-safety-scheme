CREATE PROCEDURE [dbo].[UpdateFireRiskAssessmentAsComplete]
	@ApplicationId uniqueidentifier
AS
	UPDATE dbo.ApplicationFireRiskAssessment
	SET TaskStatusId = 3
	WHERE  Id = (SELECT TOP 1 FireRiskAssessmentId from dbo.ApplicationDetails WHERE Id = @ApplicationId)
