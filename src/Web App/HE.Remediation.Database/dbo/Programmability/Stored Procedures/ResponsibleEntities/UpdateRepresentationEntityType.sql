CREATE PROCEDURE [dbo].[UpdateRepresentationEntityType]
	@ApplicationId UNIQUEIDENTIFIER,
	@ResponsibleEntityTypeId INT
AS
BEGIN
	BEGIN TRANSACTION

	DECLARE @ResponsibleEntityId UNIQUEIDENTIFIER = (
		SELECT TOP 1
			er.[Id]
		FROM [dbo].[ApplicationResponsibleEntity] er
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON er.[Id] = ad.[ResponsibleEntityId]
		WHERE ad.[Id] = @ApplicationId
	)

	DECLARE @RepresentationEntityDetailsId UNIQUEIDENTIFIER = (
		SELECT TOP 1
			re.[RepresentationEntityDetailsId]
		FROM [dbo].[ApplicationResponsibleEntity] re
		WHERE re.[Id] = @ResponsibleEntityId
	)

	SET @RepresentationEntityDetailsId = ISNULL(@RepresentationEntityDetailsId, NEWID())

	MERGE INTO [dbo].[ApplicationRepresentationEntityDetails] t
	USING(
		VALUES (@RepresentationEntityDetailsId, @ResponsibleEntityTypeId)
	) s([Id], [ResponsibleEntityTypeId])
	ON t.[Id] = s.[Id]
	WHEN MATCHED THEN
		UPDATE SET t.[ResponsibleEntityTypeId] = s.[ResponsibleEntityTypeId]
	WHEN NOT MATCHED THEN
		INSERT ([Id], [ResponsibleEntityTypeId])
		VALUES (s.[Id], s.[ResponsibleEntityTypeId]);

	UPDATE [dbo].[ApplicationResponsibleEntity]
	SET [RepresentationEntityDetailsId] = @RepresentationEntityDetailsId
	WHERE [Id] = @ResponsibleEntityId

	COMMIT TRANSACTION
END
