
CREATE PROCEDURE [dbo].[UpsertFundingStillPursuing]
	@ApplicationId UNIQUEIDENTIFIER,
	@FundingStillPursuing VARCHAR(MAX)
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

						DELETE FROM [ApplicationFundingRoutes]
						WHERE
						[ApplicationFundingRoutes].[AlternateFundingId] = @AlternateFundingId

						INSERT [ApplicationFundingRoutes] ([Id], [AlternateFundingId], [FundingRoutesTypeId])
						SELECT NEWID(), @AlternateFundingId, VALUE AS [FundingRoutesTypeId] FROM STRING_SPLIT(@FundingStillPursuing,',')

					COMMIT TRANSACTION
				END
		END
END