
CREATE PROCEDURE [dbo].[UpdateFundingRoutesCheckYourAnswers]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @AlternateFundingId AS UNIQUEIDENTIFIER

	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
		BEGIN
			SELECT @AlternateFundingId = AlternateFundingId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

			IF (@AlternateFundingId IS NOT NULL)
				BEGIN 
					BEGIN TRANSACTION
						
						DECLARE @TaskStatusId INT = (SELECT TOP 1 Id FROM [TaskStatus] WHERE [Type] = 'Completed')

						UPDATE [ApplicationAlternateFunding] SET [ApplicationAlternateFunding].[TaskStatusId] = @TaskStatusId
						WHERE [ApplicationAlternateFunding].[Id] = @AlternateFundingId 
						
					COMMIT TRANSACTION
				END
		END
END