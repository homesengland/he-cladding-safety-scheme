CREATE PROCEDURE [dbo].[GetResponsibleEntityEvidence]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		re.[Id],
		re.[OrganisationId] AS [OrganisationType],
		f.[Id],
		f.[Name],
		f.[Extension],
		f.[Size]
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
		LEFT JOIN [dbo].[ApplicationResponsibleEntityFile] ref
		ON re.[Id] = ref.[ResponsibleEntityId]
		LEFT JOIN [dbo].[File] f
		ON ref.[FileId] = f.[Id]
	WHERE ad.[Id] = @ApplicationId
END
