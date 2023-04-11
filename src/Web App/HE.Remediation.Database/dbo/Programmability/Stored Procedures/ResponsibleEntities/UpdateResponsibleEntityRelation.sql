CREATE PROCEDURE [dbo].[UpdateResponsibleEntityRelation]
	@ApplicationId UNIQUEIDENTIFIER,
	@ResponsibleEntityRelationId INT
AS
BEGIN
	UPDATE er
	SET
		er.[BuildingRelationshipId] = @ResponsibleEntityRelationId
	FROM
		[dbo].[ApplicationResponsibleEntity] er
			INNER JOIN
		[dbo].[ApplicationDetails] ad
			ON er.[Id] = ad.[ResponsibleEntityId]
	WHERE
		ad.[Id] = @ApplicationId
END