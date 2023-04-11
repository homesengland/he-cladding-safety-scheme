CREATE PROCEDURE [dbo].[GetBuildingAddress]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
		ABD.[NonResidentialUnits],
		ADDR.[NameNumber],
		ADDR.[AddressLine1],
		ADDR.[AddressLine2],
		ADDR.[City],
		ADDR.[LocalAuthority],
		ADDR.[County],
		ADDR.[Postcode]
    FROM
		[Address] ADDR
			INNER JOIN
        [ApplicationBuildingDetails] ABD
			ON ADDR.Id = ABD.AddressId
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.[Id] = AD.BuildingDetailsId
    WHERE
        AD.[Id] = @ApplicationId
END
GO