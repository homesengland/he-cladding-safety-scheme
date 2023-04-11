CREATE PROCEDURE [dbo].[GetFreeholderAddress]
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
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ADDR.[Id] = AREF.[AddressId]
			INNER JOIN
        [ApplicationResponsibleEntity] ARE
			ON AREF.[Id] = ARE.[FreeholderId]
			INNER JOIN
		[ApplicationDetails] AD
			ON ARE.[Id] = AD.[ResponsibleEntityId]
	WHERE
		AD.[Id] = @ApplicationId
END
GO