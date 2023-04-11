CREATE PROCEDURE [dbo].[UpdateNonResidentialUnits]
    @ApplicationId UNIQUEIDENTIFIER,
    @NonResidentialUnitsCount INT
AS
BEGIN
    UPDATE
		[ApplicationBuildingDetails]
	SET
		[NonResidentialUnitsCount] = @NonResidentialUnitsCount
	FROM
		[ApplicationBuildingDetails] ABD
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.Id = AD.BuildingDetailsId
	WHERE
		AD.[Id] = @ApplicationId
END
GO