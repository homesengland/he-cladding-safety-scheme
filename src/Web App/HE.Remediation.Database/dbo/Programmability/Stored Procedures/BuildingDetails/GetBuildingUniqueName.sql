CREATE PROCEDURE [dbo].[GetBuildingUniqueName]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        [UniqueName]
    FROM
        [ApplicationBuildingDetails] ABD
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.Id = AD.BuildingDetailsId
    WHERE
        AD.[Id] = @ApplicationId
END