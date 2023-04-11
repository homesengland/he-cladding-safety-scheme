CREATE PROCEDURE [dbo].[DeleteFraewForApplication]
	@ApplicationId uniqueidentifier
AS
	UPDATE dbo.ApplicationFireRiskAssessment
	SET FraewFileId = NULL
	WHERE  Id = (SELECT TOP 1 FireRiskAssessmentId from dbo.ApplicationDetails WHERE Id = @ApplicationId)
