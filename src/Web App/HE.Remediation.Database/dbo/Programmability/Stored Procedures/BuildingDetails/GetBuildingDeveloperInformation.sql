CREATE PROCEDURE [dbo].[GetBuildingDeveloperInformation]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		bd.[OriginalDeveloperKnown] AS [DoYouKnowOriginalDeveloper],
		dev.[Name] AS [OrganisationName],
		addr.[NameNumber],
		addr.[AddressLine1],
		addr.[AddressLine2],
		addr.[City],
		addr.[Postcode]
	FROM [dbo].[ApplicationBuildingDetails] bd
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON bd.[Id] = ad.[BuildingDetailsId]
		LEFT JOIN [dbo].[ApplicationDeveloper] dev
		ON bd.[DeveloperId] = dev.[Id]
		LEFT JOIN [dbo].[Address] addr
		ON dev.[AddressId] = addr.[Id]
	WHERE ad.[Id] = @ApplicationId
END
