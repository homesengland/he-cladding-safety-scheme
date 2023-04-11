CREATE PROCEDURE [UpdateBuildingAddress]
	@ApplicationId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(10),
	@LocalAuthority NVARCHAR(150)
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
		[Postcode] = @Postcode,
		[LocalAuthority] = @LocalAuthority
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationBuildingDetails] ABD
			ON AD.[BuildingDetailsId] = ABD.[Id]
			INNER JOIN
		[Address] ADDR
			ON ABD.[AddressId] = ADDR.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO