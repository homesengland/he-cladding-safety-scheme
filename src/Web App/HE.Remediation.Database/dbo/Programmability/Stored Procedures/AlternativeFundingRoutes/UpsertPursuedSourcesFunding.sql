
CREATE PROCEDURE [dbo].[UpsertPursuedSourcesFunding]
	@ApplicationId UNIQUEIDENTIFIER,
	@PursuedSourcesFunding TINYINT
AS
BEGIN
	
	DECLARE @ApplicationOtherSourcesPursuedId UNIQUEIDENTIFIER
	DECLARE @AlternateFundingId AS UNIQUEIDENTIFIER
	DECLARE @ExhaustedAllRoutesTypeId AS INT = (SELECT [Id] FROM [ApplicationOtherSourcesPursuedType] WHERE [ApplicationOtherSourcesPursuedType].[Type] = 'Yes, I have exhausted all other routes of funding and am unable to claim any funding')
	DECLARE @NotExhaustedAllRoutesTypeId AS INT = (SELECT [Id] FROM [ApplicationOtherSourcesPursuedType] WHERE [ApplicationOtherSourcesPursuedType].[Type] = 'No, there are still other routes of funding that I could pursue')
	DECLARE @InProgressTaskStatusId INT = (SELECT TOP 1 Id FROM [TaskStatus] WHERE [Type] = 'In Progress')

	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
		BEGIN
			SELECT @AlternateFundingId = AlternateFundingId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId

			IF (@AlternateFundingId IS NULL)
				BEGIN
					BEGIN TRANSACTION
						SET @AlternateFundingId = NEWID()
						INSERT INTO 
							[ApplicationAlternateFunding] (Id, OtherSourcesPursuedTypeId, TaskStatusId) 
						VALUES 
							(@AlternateFundingId, @PursuedSourcesFunding, @InProgressTaskStatusId)

						UPDATE 
							[ApplicationDetails] 
						SET 
							[ApplicationDetails].AlternateFundingId = @AlternateFundingId
						WHERE 
							[ApplicationDetails].[Id] = @ApplicationId
					COMMIT TRANSACTION
				END
		
			IF(@PursuedSourcesFunding = @ExhaustedAllRoutesTypeId OR @PursuedSourcesFunding = @NotExhaustedAllRoutesTypeId)
				BEGIN
					DECLARE @CompletedTaskStatusId INT = (SELECT TOP 1 Id FROM [TaskStatus] WHERE [Type] = 'Completed')

					BEGIN TRANSACTION
						DELETE 
							[ApplicationFundingRoutes] 
						WHERE 
							[ApplicationFundingRoutes].[AlternateFundingId] = @AlternateFundingId
						
						UPDATE 
							[ApplicationAlternateFunding] 
						SET 
							[ApplicationAlternateFunding].OtherSourcesPursuedTypeId = @PursuedSourcesFunding,
							[ApplicationAlternateFunding].TaskStatusId = @CompletedTaskStatusId
						WHERE 
							[ApplicationAlternateFunding].[Id] = @AlternateFundingId

					COMMIT TRANSACTION
				END
			ELSE
				BEGIN
					UPDATE 
							[ApplicationAlternateFunding] 
						SET 
							[ApplicationAlternateFunding].OtherSourcesPursuedTypeId = @PursuedSourcesFunding,
							[ApplicationAlternateFunding].TaskStatusId = @InProgressTaskStatusId
						WHERE 
							[ApplicationAlternateFunding].[Id] = @AlternateFundingId
				END
		END
END