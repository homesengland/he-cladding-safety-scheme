
CREATE PROCEDURE [dbo].[InsertOrUpdateRepresentativeType]
	@ApplicationId UNIQUEIDENTIFIER,
	@RepresentationTypeId INT
AS
BEGIN
	BEGIN TRANSACTION

	DECLARE @ResponsibleEntityId UNIQUEIDENTIFIER = (
				SELECT TOP 1 
					are.[Id]
				FROM [dbo].[ApplicationResponsibleEntity] are
					LEFT JOIN [dbo].[ApplicationDetails] ad
					ON are.[Id] = ad.[ResponsibleEntityId]
				WHERE ad.[Id] = @ApplicationId
			),
			@InProgressId INT = 2;
	
	SET @ResponsibleEntityId = ISNULL(@ResponsibleEntityId, NEWID())


	MERGE INTO [dbo].[ApplicationResponsibleEntity] t
	USING (
		VALUES(@ResponsibleEntityId, @RepresentationTypeId)
	) s([Id], [RepresentationTypeId])
	ON t.[Id] = s.[Id]
	WHEN MATCHED THEN
	UPDATE SET t.[RepresentationTypeId] = s.[RepresentationTypeId]
	WHEN NOT MATCHED THEN
	INSERT ([Id], [RepresentationTypeId], [TaskStatusId])
	VALUES (s.[Id], s.[RepresentationTypeId], @InProgressId);

	UPDATE [dbo].[ApplicationDetails]
	SET [ResponsibleEntityId] = @ResponsibleEntityId
	WHERE [Id] = @ApplicationId
	
	COMMIT TRANSACTION
END
