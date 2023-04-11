CREATE PROCEDURE [dbo].[UpdateBuildingDetailsId]
    @ApplicationId UNIQUEIDENTIFIER,
    @BuildingDetailsId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE
		[ApplicationDetails] 
	SET
		[BuildingDetailsId] = @BuildingDetailsId
	WHERE
		[Id] = @ApplicationId
END
GO