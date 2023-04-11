CREATE PROCEDURE [dbo].[GetRepresentationCompanyOrIndividualAddress]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		red.[ResponsibleEntityTypeId] AS [ResponsibleEntityType],
		COALESCE(radd.[NameNumber], cadd.[NameNumber]) AS [NameNumber],
		COALESCE(radd.[AddressLine1], cadd.[AddressLine1]) AS [AddressLine1],
		COALESCE(radd.[AddressLine2], cadd.[AddressLine2]) AS [AddressLine2],
		COALESCE(radd.[City], cadd.[City]) AS [City],
		COALESCE(radd.[County], cadd.[County]) AS [County],
		COALESCE(radd.[Postcode], cadd.[Postcode]) AS [Postcode]
	FROM [dbo].[ApplicationDetails] ad
		LEFT JOIN [dbo].[CompanyDetails] cd
		ON cd.[Id] = ad.[CompanyId]
		LEFT JOIN [dbo].[CompanyAddress] ca
		ON cd.[Id] = ca.[CompanyId]
		LEFT JOIN [dbo].[Address] cadd
		ON ca.[AddressId] = cadd.[Id]
		LEFT JOIN [dbo].[ApplicationResponsibleEntity] re
		ON ad.[ResponsibleEntityId] = re.[Id]
		LEFT JOIN [dbo].[ApplicationRepresentationEntityDetails] red
		ON red.[Id] = re.[RepresentationEntityDetailsId]
		LEFT JOIN [dbo].[Address] radd
		ON red.[AddressId] = radd.[Id]
		WHERE [ad].Id=@ApplicationId
END
