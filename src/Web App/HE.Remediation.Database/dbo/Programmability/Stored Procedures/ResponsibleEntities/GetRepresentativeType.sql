CREATE PROCEDURE [dbo].[GetRepresentativeType]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		are.[RepresentationTypeId]
	FROM [dbo].[ApplicationResponsibleEntity] are
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON are.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
