CREATE PROCEDURE [dbo].[GetRepresentationEntityType]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		red.[ResponsibleEntityTypeId]
	FROM [dbo].[ApplicationResponsibleEntity] er
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON er.[Id] = ad.[ResponsibleEntityId]
		LEFT JOIN [dbo].[ApplicationRepresentationEntityDetails] red
		ON red.[Id] = er.[RepresentationEntityDetailsId]
	WHERE ad.[Id] = @ApplicationId
END
