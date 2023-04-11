CREATE PROCEDURE [dbo].[InsertResponsibleEntitiesEvidence]
	@ApplicationId UNIQUEIDENTIFIER,
	@FileId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @ResponsibleEntityId UNIQUEIDENTIFIER = (
		SELECT TOP 1
			re.[Id]
		FROM [dbo].[ApplicationResponsibleEntity] re
			INNER JOIN [dbo].[ApplicationDetails] ad
			ON  re.[Id] = ad.[ResponsibleEntityId]
		WHERE ad.[Id] = @ApplicationId
	);

	INSERT INTO [dbo].[ApplicationResponsibleEntityFile] ([FileId], [ResponsibleEntityId])
	VALUES (@FileId, @ResponsibleEntityId);
END
