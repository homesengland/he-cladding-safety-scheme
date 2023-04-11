








CREATE PROCEDURE [dbo].[InsertOrUpdateAppraisalSurveyDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@FireRiskAssessorId INT = NULL,
	@DateOfInstruction DateTime2(7),
	@SurveyDate DateTime2(7)
AS
BEGIN
	DECLARE @FireRiskAssessmentId AS UNIQUEIDENTIFIER
	SELECT @FireRiskAssessmentId = FireRiskAssessmentId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

	DECLARE @ApplicationFireRiskAssessmentAppraisalSurveyDetailsId AS UNIQUEIDENTIFIER
	SELECT @ApplicationFireRiskAssessmentAppraisalSurveyDetailsId = Id FROM [ApplicationFireRiskAssessmentAppraisalSurveyDetails] WHERE [ApplicationFireRiskAssessmentAppraisalSurveyDetails].[FireRiskAssessmentId] = @FireRiskAssessmentId

	IF (@ApplicationFireRiskAssessmentAppraisalSurveyDetailsId IS NULL)
		BEGIN
			SET @ApplicationFireRiskAssessmentAppraisalSurveyDetailsId = NEWID()  
			INSERT INTO [ApplicationFireRiskAssessmentAppraisalSurveyDetails] (Id, FireRiskAssessmentId, FireRiskAssessorListId, DateOfInstruction, SurveyDate) VALUES (@ApplicationFireRiskAssessmentAppraisalSurveyDetailsId, @FireRiskAssessmentId, @FireRiskAssessorId, @DateOfInstruction, @SurveyDate)
			UPDATE [ApplicationFireRiskAssessment] SET AppraisalServiceDetailsId = @ApplicationFireRiskAssessmentAppraisalSurveyDetailsId WHERE Id = @FireRiskAssessmentId
		END
	ELSE
		BEGIN
			UPDATE [ApplicationFireRiskAssessmentAppraisalSurveyDetails] SET
				[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[FireRiskAssessorListId] = @FireRiskAssessorId,
				[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[DateOfInstruction] = @DateOfInstruction,
				[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[SurveyDate] = @SurveyDate
			WHERE 
				[ApplicationFireRiskAssessmentAppraisalSurveyDetails].[Id] = @ApplicationFireRiskAssessmentAppraisalSurveyDetailsId
		END
END