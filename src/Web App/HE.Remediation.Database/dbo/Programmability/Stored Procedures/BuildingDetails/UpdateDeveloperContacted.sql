CREATE PROCEDURE [dbo].[UpdateDeveloperContacted]
	@ApplicationId UNIQUEIDENTIFIER,
	@HasDeveloperBeenContactedAboutRemediation BIT
AS
BEGIN
	UPDATE dev
	SET dev.[DeveloperContacted] = @HasDeveloperBeenContactedAboutRemediation
	FROM [dbo].[ApplicationDeveloper] dev
		INNER JOIN [dbo].[ApplicationBuildingDetails] bd
		ON dev.[Id] = bd.[DeveloperId]
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON bd.[Id] = ad.[BuildingDetailsId]
	WHERE ad.[Id] = @ApplicationId
END
