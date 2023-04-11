CREATE PROCEDURE [dbo].[InsertFraewForApplication]
	@ApplicationId uniqueidentifier,
	@FileId uniqueidentifier
AS
	UPDATE dbo.ApplicationFireRiskAssessment
	SET FraewFileId = @FileId
	WHERE  Id = (SELECT TOP 1 FireRiskAssessmentId from dbo.ApplicationDetails WHERE Id = @ApplicationId)
