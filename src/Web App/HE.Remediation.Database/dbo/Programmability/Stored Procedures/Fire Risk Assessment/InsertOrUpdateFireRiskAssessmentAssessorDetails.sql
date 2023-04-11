CREATE PROCEDURE [dbo].[InsertOrUpdateFireRiskAssessmentAssessorDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(150),
	@LastName NVARCHAR(150),
	@CompanyName NVARCHAR(150),
	@CompanyNumber NVARCHAR(150),
	@EmailAddress NVARCHAR(150),
	@Telephone NVARCHAR(150)
AS
	DECLARE @FireRiskAssessmentId AS UNIQUEIDENTIFIER
	DECLARE @FireRiskAssessmentDetailsId as UNIQUEIDENTIFIER
		
	SELECT 
	@FireRiskAssessmentDetailsId = fraad.[Id]	
	FROM [ApplicationFireRiskAssessmentAssessorDetails] fraad
	INNER JOIN [ApplicationFireRiskAssessment] fra
	ON fraad.[FireRiskAssessmentId]=fra.[Id]
	INNER JOIN [ApplicationDetails] ad
	ON ad.[FireRiskAssessmentId]=fra.[Id]
	WHERE 
	ad.[Id] = @ApplicationId

	IF (@FireRiskAssessmentDetailsId IS NULL)
		BEGIN			 
			BEGIN TRANSACTION
				
				SET @FireRiskAssessmentDetailsId = NEWID()  

				SELECT @FireRiskAssessmentId = FireRiskAssessmentId
				FROM
				[ApplicationDetails] ad
				WHERE 
				ad.id=@ApplicationId
				
				INSERT INTO [ApplicationFireRiskAssessmentAssessorDetails]
				([Id], [FireRiskAssessmentId], [FirstName], [LastName], [CompanyName], 
				[CompanyNumber], [EmailAddress], [Telephone])
				VALUES 
				(@FireRiskAssessmentDetailsId,
				@FireRiskAssessmentId,
				@FirstName,
				@LastName,
				@CompanyName,
				@CompanyNumber,
				@EmailAddress,
				@Telephone)				

				UPDATE 
				[ApplicationFireRiskAssessment]
				SET 
				[AssessorDetailsId] = @FireRiskAssessmentDetailsId,
				[TaskStatusId] = 3
				WHERE 
				[ApplicationFireRiskAssessment].[Id] = @FireRiskAssessmentId

			COMMIT TRANSACTION
		END
	ELSE
		BEGIN
			UPDATE 
			[ApplicationFireRiskAssessmentAssessorDetails] 
			SET 
			[ApplicationFireRiskAssessmentAssessorDetails].[FirstName] = @FirstName,
			[ApplicationFireRiskAssessmentAssessorDetails].[LastName] = @LastName,
			[ApplicationFireRiskAssessmentAssessorDetails].[CompanyName] = @CompanyName,
			[ApplicationFireRiskAssessmentAssessorDetails].[CompanyNumber] = @CompanyNumber,
			[ApplicationFireRiskAssessmentAssessorDetails].[EmailAddress] = @EmailAddress,
			[ApplicationFireRiskAssessmentAssessorDetails].[Telephone] = @Telephone
			WHERE 
			[ApplicationFireRiskAssessmentAssessorDetails].[Id] = @FireRiskAssessmentDetailsId
		END

