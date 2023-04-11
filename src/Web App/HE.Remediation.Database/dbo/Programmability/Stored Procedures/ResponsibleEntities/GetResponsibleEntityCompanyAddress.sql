CREATE PROCEDURE [dbo].[GetResponsibleEntityCompanyAddress]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
		ADDR.[NameNumber],
		ADDR.[AddressLine1],
		ADDR.[AddressLine2],
		ADDR.[City],
		ADDR.[County],
		ADDR.[Postcode]
    FROM
		[Address] ADDR
			INNER JOIN
        [ApplicationResponsibleEntity] ARE
			ON ADDR.[Id] = ARE.[AddressId]
			INNER JOIN
		[ApplicationDetails] AD
			ON ARE.[Id] = AD.[ResponsibleEntityId]
	WHERE
		AD.[Id] = @ApplicationId
END