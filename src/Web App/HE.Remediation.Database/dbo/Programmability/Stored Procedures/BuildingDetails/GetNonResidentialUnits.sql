CREATE PROCEDURE [dbo].[GetNonResidentialUnits]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        ABD.[NonResidentialUnitsCount]
    FROM
        [ApplicationBuildingDetails] ABD
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.[Id] = AD.BuildingDetailsId
    WHERE
        AD.[Id] = @ApplicationId
END
GO