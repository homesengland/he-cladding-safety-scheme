CREATE PROCEDURE [dbo].[GetBuildingHeight]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		bd.[Storeys] AS [NumberOfStoreys],
		bd.[CorrectHeightConfirmedDate]
	FROM
		[dbo].[ApplicationBuildingDetails] bd
			INNER JOIN
		[dbo].[ApplicationDetails] ad
			ON bd.[Id] = ad.[BuildingDetailsId]
	WHERE ad.[Id] = @ApplicationId
END