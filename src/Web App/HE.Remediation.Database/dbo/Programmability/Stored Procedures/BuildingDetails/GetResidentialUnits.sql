CREATE PROCEDURE [dbo].[GetResidentialUnits]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        ABD.[ResidentialUnitsCount],
		ABD.[NonResidentialUnits]
    FROM
        [ApplicationBuildingDetails] ABD
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.[Id] = AD.BuildingDetailsId
    WHERE
        AD.[Id] = @ApplicationId
END
GO