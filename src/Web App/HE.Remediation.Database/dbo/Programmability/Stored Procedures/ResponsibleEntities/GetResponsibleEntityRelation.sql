CREATE PROCEDURE [dbo].[GetResponsibleEntityRelation]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		er.[BuildingRelationshipId] AS [ResponsibleEntityRelation],
		er.[RepresentationTypeId] AS [RepresentationType]
	FROM [dbo].[ApplicationResponsibleEntity] er
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON er.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END