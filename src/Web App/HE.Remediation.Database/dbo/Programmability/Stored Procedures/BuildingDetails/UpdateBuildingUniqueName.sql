CREATE PROCEDURE [dbo].[UpdateBuildingUniqueName]
    @ApplicationId UNIQUEIDENTIFIER,
    @UniqueName NVARCHAR(max)
AS
BEGIN
    UPDATE
		[ApplicationBuildingDetails]
	SET
		[UniqueName] = @UniqueName
	FROM
		[ApplicationBuildingDetails] ABD
			INNER JOIN
		[ApplicationDetails] AD
			ON ABD.Id = AD.BuildingDetailsId
	WHERE
		AD.[Id] = @ApplicationId
END
GO