CREATE PROCEDURE [dbo].[InsertOrUpdateFireRiskCompletedStatus]
	@ApplicationId UNIQUEIDENTIFIER,
	@IsAppraisalCompleted BIT
AS
	DECLARE @RiskAssessmentId AS UNIQUEIDENTIFIER
	DECLARE @ApplicationFireRiskAssessment AS UNIQUEIDENTIFIER

	SELECT @RiskAssessmentId = FireRiskAssessmentId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

	IF (@RiskAssessmentId IS NULL)
		BEGIN			 
			BEGIN TRANSACTION
				
				SET @ApplicationFireRiskAssessment = NEWID()  
				
				INSERT INTO [ApplicationFireRiskAssessment] (Id, FireRiskCompleted, TaskStatusId) 
				VALUES 
				(@ApplicationFireRiskAssessment, @IsAppraisalCompleted, 2)

				UPDATE 
				[ApplicationDetails]
				SET 
				[FireRiskAssessmentId] = @ApplicationFireRiskAssessment 
				WHERE 
				[Id] = @ApplicationID

			COMMIT TRANSACTION
		END
	ELSE
		BEGIN
			UPDATE 
			[ApplicationFireRiskAssessment] 
			SET 
			[ApplicationFireRiskAssessment].[FireRiskCompleted] = @IsAppraisalCompleted 
			WHERE 
			[ApplicationFireRiskAssessment].[Id] = @RiskAssessmentId
		END
RETURN 0
