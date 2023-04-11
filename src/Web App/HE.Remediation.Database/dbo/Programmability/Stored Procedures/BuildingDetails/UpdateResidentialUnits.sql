CREATE PROCEDURE [dbo].[UpdateResidentialUnits]
    @ApplicationId UNIQUEIDENTIFIER,
    @ResidentialUnitsCount INT,
	@NonResidentialUnits BIT
AS
BEGIN
	IF (@NonResidentialUnits = 1)

		UPDATE
			[ApplicationBuildingDetails]
		SET
			[ResidentialUnitsCount] = @ResidentialUnitsCount,
			[NonResidentialUnits] = @NonResidentialUnits
		FROM
			[ApplicationBuildingDetails] ABD
				INNER JOIN
			[ApplicationDetails] AD
				ON ABD.Id = AD.BuildingDetailsId
		WHERE
			AD.[Id] = @ApplicationId

	ELSE

		UPDATE
			[ApplicationBuildingDetails]
		SET
			[ResidentialUnitsCount] = @ResidentialUnitsCount,
			[NonResidentialUnits] = @NonResidentialUnits,
			[NonResidentialUnitsCount] = NULL
		FROM
			[ApplicationBuildingDetails] ABD
				INNER JOIN
			[ApplicationDetails] AD
				ON ABD.Id = AD.BuildingDetailsId
		WHERE
			AD.[Id] = @ApplicationId

END
GO