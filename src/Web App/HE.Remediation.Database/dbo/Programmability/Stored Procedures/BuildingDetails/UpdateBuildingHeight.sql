CREATE PROCEDURE [dbo].[UpdateBuildingHeight]
	@ApplicationId UNIQUEIDENTIFIER,
	@NumberOfStoreys INT
AS
BEGIN
	UPDATE bd
	SET 
		bd.[Storeys] = @NumberOfStoreys,
		bd.[CorrectHeightConfirmedDate] = GETDATE()
	FROM [dbo].[ApplicationBuildingDetails] bd
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON bd.[Id] = ad.[BuildingDetailsId]
	WHERE ad.[Id] = @ApplicationId
END
