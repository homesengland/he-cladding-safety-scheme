CREATE PROCEDURE [dbo].[InsertBuildingUniqueName]
    @BuildingDetailsId UNIQUEIDENTIFIER,
    @UniqueName NVARCHAR(max)
AS
BEGIN
    INSERT INTO [ApplicationBuildingDetails]
		(
			[Id],
			[UniqueName],
			[TaskStatusId]
		)
    VALUES
        (
			@BuildingDetailsId,
			@UniqueName,
			2
		)
END
GO