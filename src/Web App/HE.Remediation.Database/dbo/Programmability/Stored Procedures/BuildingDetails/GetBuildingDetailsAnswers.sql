
CREATE PROCEDURE [dbo].[GetBuildingDetailsAnswers]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
SELECT 
	ABD.UniqueName AS BuildingUniqueName,
	[ResidentialUnitsCount],
	[NonResidentialUnits],
	[NonResidentialUnitsCount],
	ADDR1.[NameNumber] AS 'BuildingNameNumber',
	ADDR1.[AddressLine1] AS 'BuildingAddressLine1',
	ADDR1.[AddressLine2] AS 'BuildingAddressline2',
	ADDR1.[City] AS 'BuildingCity',
	ADDR1.[LocalAuthority] AS 'BuildingLocalAuthority',
	ADDR1.[County] AS 'BuildingCounty',
	ADDR1.[Postcode] AS 'BuildingPostcode',
	[PartOfDevelopment],
	[Storeys],
	[CorrectHeightConfirmedDate],
	[OriginalDeveloperKnown],
	ADEV.[Name] AS 'DeveloperCompanyName',
	ADDR2.[NameNumber] AS 'DeveloperNameNumber',
	ADDR2.[AddressLine1] AS 'DeveloperAddressLine1',
	ADDR2.[AddressLine2] AS 'DeveloperAddressline2',
	ADDR2.[City] AS 'DeveloperCity',
	ADDR2.[LocalAuthority] AS 'DeveloperLocalAuthority',
	ADDR2.[County] AS 'DeveloperCounty',
	ADDR2.[Postcode] AS 'DeveloperPostcode',
	ADIBT.[Type] AS 'DeveloperStillInBusiness',
	ADEV.[DeveloperContacted] AS 'DeveloperContacted'
FROM
	[ApplicationDetails] AD
		INNER JOIN
	[ApplicationBuildingDetails] ABD
		ON AD.[BuildingDetailsId] = ABD.[Id]
		LEFT JOIN
	[Address] ADDR1
		ON ABD.[AddressId] = ADDR1.[Id]
		LEFT JOIN
	[ApplicationDeveloper] ADEV
		ON ABD.[DeveloperId] = ADEV.[Id]
		LEFT JOIN
	[Address] ADDR2
		ON ADEV.[AddressId] = ADDR2.[Id]
		LEFT JOIN
		[ApplicationDeveloperInBusinessType] ADIBT
			ON ADEV.[StillInBusinessId] = ADIBT.[Id]
    WHERE
        AD.[Id] = @ApplicationId
END
GO