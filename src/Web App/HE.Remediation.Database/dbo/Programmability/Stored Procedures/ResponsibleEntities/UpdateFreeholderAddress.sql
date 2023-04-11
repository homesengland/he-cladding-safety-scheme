CREATE PROCEDURE [dbo].[UpdateFreeholderAddress]
	@ApplicationId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(10)
AS
BEGIN
    UPDATE
		[Address] 
	SET
		[NameNumber] = @NameNumber,
		[AddressLine1] = @AddressLine1,
		[AddressLine2] = @AddressLine2,
		[City] = @City,
		[County] = @County,
		[Postcode] = @Postcode
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
			INNER JOIN
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ARE.[FreeholderId] = AREF.[Id]
			INNER JOIN
		[Address] ADDR
			ON AREF.[AddressId] = ADDR.[Id]	WHERE
		AD.[Id] = @ApplicationId
END
GO