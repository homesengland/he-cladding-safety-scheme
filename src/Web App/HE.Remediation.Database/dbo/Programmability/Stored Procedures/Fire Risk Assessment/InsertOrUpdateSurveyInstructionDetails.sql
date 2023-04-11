






CREATE PROCEDURE [dbo].[InsertOrUpdateSurveyInstructionDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@FireRiskAssessorId INT,
	@DateOfInstruction DateTime2(7)
AS
BEGIN
	DECLARE @FireRiskAssessmentId AS UNIQUEIDENTIFIER
	SELECT @FireRiskAssessmentId = FireRiskAssessmentId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

	DECLARE @ApplicationFireRiskAssessmentSurveyInstructionDetailsId AS UNIQUEIDENTIFIER
	SELECT @ApplicationFireRiskAssessmentSurveyInstructionDetailsId = Id FROM [ApplicationFireRiskAssessmentSurveyInstructionDetails] WHERE [ApplicationFireRiskAssessmentSurveyInstructionDetails].[FireRiskAssessmentId] = @FireRiskAssessmentId

	IF (@ApplicationFireRiskAssessmentSurveyInstructionDetailsId IS NULL)
		BEGIN
			SET @ApplicationFireRiskAssessmentSurveyInstructionDetailsId = NEWID()  
			INSERT INTO [ApplicationFireRiskAssessmentSurveyInstructionDetails] (Id, FireRiskAssessmentId, FireRiskAssessorListId, DateOfInstruction) VALUES (@ApplicationFireRiskAssessmentSurveyInstructionDetailsId, @FireRiskAssessmentId, @FireRiskAssessorId, @DateOfInstruction)
			UPDATE [ApplicationFireRiskAssessment] SET SurveyInstructionDetailsId = @ApplicationFireRiskAssessmentSurveyInstructionDetailsId WHERE Id = @FireRiskAssessmentId
		END
	ELSE
		BEGIN
			UPDATE [ApplicationFireRiskAssessmentSurveyInstructionDetails] SET
				[ApplicationFireRiskAssessmentSurveyInstructionDetails].[FireRiskAssessorListId] = @FireRiskAssessorId,
				[ApplicationFireRiskAssessmentSurveyInstructionDetails].[DateOfInstruction] = @DateOfInstruction
			WHERE 
				[ApplicationFireRiskAssessmentSurveyInstructionDetails].[Id] = @ApplicationFireRiskAssessmentSurveyInstructionDetailsId
		END
END