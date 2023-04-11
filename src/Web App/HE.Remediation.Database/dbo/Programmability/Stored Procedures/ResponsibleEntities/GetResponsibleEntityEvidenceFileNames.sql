CREATE PROCEDURE [dbo].[GetResponsibleEntityEvidenceFileNames]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT DISTINCT
		f.[Name]
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
		LEFT JOIN [dbo].[ApplicationResponsibleEntityFile] ref
		ON re.[Id] = ref.[ResponsibleEntityId]
		LEFT JOIN [dbo].[File] f
		ON ref.[FileId] = f.[Id]
	WHERE ad.[Id] = @ApplicationId
END