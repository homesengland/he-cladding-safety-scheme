CREATE PROCEDURE [dbo].[GetBuildingPartOfDevelopment]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
		ABD.[PartOfDevelopment]
    FROM
        [ApplicationBuildingDetails] ABD
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.[Id] = AD.BuildingDetailsId
    WHERE
        AD.[Id] = @ApplicationId
END
GO