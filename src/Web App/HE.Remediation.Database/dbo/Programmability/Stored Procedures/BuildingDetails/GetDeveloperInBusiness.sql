CREATE PROCEDURE [dbo].[GetDeveloperInBusiness]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		dev.[StillInBusinessId]
	FROM [dbo].[ApplicationDeveloper] dev
		INNER JOIN [dbo].[ApplicationBuildingDetails] bd
		ON dev.[Id] = bd.[DeveloperId]
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON bd.[Id] = ad.[BuildingDetailsId]
	WHERE ad.[Id] = @ApplicationId
END
