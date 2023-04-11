CREATE PROCEDURE [dbo].[GetRepresentationCompanyOrIndividualDetails]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		red.[ResponsibleEntityTypeId] AS [ResponsibleEntityType],
		COALESCE(red.[CompanyName], cd.[Name]) AS [CompanyName],
		COALESCE(red.[CompanyRegistration], cd.[RegistrationNumber]) AS [CompanyRegistration],
		COALESCE(red.[FirstName], u.[FirstName]) AS [FirstName],
		COALESCE(red.[LastName], u.[LastName]) AS [LastName],
		COALESCE(red.[EmailAddress], u.[EmailAddress]) AS [EmailAddress],
		COALESCE(red.[ContactNumber], u.[ContactNumber]) AS [ContactNumber]
	FROM [dbo].[ApplicationDetails] ad
		INNER JOIN [dbo].[ApplicationResponsibleEntity] re
		ON ad.[ResponsibleEntityId] = re.[Id]
		INNER JOIN [dbo].[ApplicationRepresentationEntityDetails] red
		ON re.[RepresentationEntityDetailsId] = red.[Id]
		LEFT JOIN [dbo].[CompanyDetails] cd
		ON ad.[CompanyId] = cd.[Id]
		LEFT JOIN [dbo].[CompanyAddress] ca
		ON cd.[Id] = ca.[CompanyId]
		LEFT JOIN [dbo].[Address] addr
		ON ca.[AddressId] = addr.[Id]
		LEFT JOIN [dbo].[UserDetails] u
		ON ad.[UserId] = u.[UserId]
	WHERE ad.[Id] = @ApplicationId
END
