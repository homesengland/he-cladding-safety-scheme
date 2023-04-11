CREATE PROCEDURE [dbo].[UpdateDeveloperInBusiness]
	@ApplicationId UNIQUEIDENTIFIER,
	@IsOriginalDeveloperStillInBusiness INT
AS
BEGIN
	UPDATE dev
	SET dev.StillInBusinessId = @IsOriginalDeveloperStillInBusiness
	FROM [dbo].[ApplicationDeveloper] dev
		INNER JOIN [dbo].[ApplicationBuildingDetails] bd
		ON dev.[Id] = bd.[DeveloperId]
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON bd.[Id] = ad.[BuildingDetailsId]
	WHERE ad.[Id] = @ApplicationId

END
