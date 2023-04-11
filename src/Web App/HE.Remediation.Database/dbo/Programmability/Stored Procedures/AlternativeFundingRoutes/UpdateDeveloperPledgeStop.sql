CREATE PROCEDURE [dbo].[UpdateDeveloperPledgeStop]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
	BEGIN
		BEGIN TRANSACTION

			DECLARE @AlternateFundingId UNIQUEIDENTIFIER = (SELECT AlternateFundingId FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
			DECLARE @RejectedStatusId INT = (SELECT TOP 1 Id FROM [ApplicationStatus] WHERE [ApplicationStatus].[Type] = 'Not Eligible')
			DECLARE @TaskStatusId INT = (SELECT TOP 1 Id FROM [TaskStatus] WHERE [Type] = 'Completed')

			UPDATE 
				[ApplicationAlternateFunding] 
			SET 
				[ApplicationAlternateFunding].[TaskStatusId] = @TaskStatusId
			WHERE 
				[ApplicationAlternateFunding].[Id] = @AlternateFundingId 

			UPDATE
				[ApplicationDetails] 
			SET
				[StatusId] = @RejectedStatusId
			WHERE
				[Id] = @ApplicationId

			INSERT 
				[ApplicationStateHistory] ([Id], [AppId], [StatusId], [StateChanged])
			VALUES
				(NEWID(), @ApplicationId, @RejectedStatusId,  GETDATE())

		COMMIT TRANSACTION
	END
END