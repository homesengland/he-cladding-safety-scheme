CREATE PROCEDURE [dbo].[GetConfirmedNotViable]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		re.[OrganisationId] AS [OrganisationType],
		re.[IsConfirmedNotViable]
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
