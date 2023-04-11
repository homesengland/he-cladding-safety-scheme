CREATE PROCEDURE [dbo].[GetResponsibleEntityCompanyType]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		re.[OrganisationId] AS [OrganisationType],
		re.[OrganisationSubTypeId] AS [OrganisationSubType],
		re.[OrganisationSubTypeDescription],
		re.[BuildingRelationshipId] AS [ResponsibleEntityRelationType]
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
