CREATE PROCEDURE [dbo].[UpdateBuildingDetailsAddressId]
    @ApplicationId UNIQUEIDENTIFIER,
    @AddressId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE
		[ApplicationBuildingDetails] 
	SET
		[AddressId] = @AddressId
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationBuildingDetails] ABD
			ON AD.[BuildingDetailsId] = ABD.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO