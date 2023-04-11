CREATE PROCEDURE [dbo].[GetStageDiagram]
    @ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	IF EXISTS (SELECT [Id] FROM [ApplicationDetails] WHERE [ApplicationDetails].[Id] = @ApplicationId)
		BEGIN
			SELECT 
			ReferenceNumber AS ApplicationNumber, 
			ISNULL(ApplicationBuildingDetails.UniqueName, '') AS UniqueBuildingName,
			StageId AS Stage,
			CreationDate AS DateCreated,
			StatusId AS [Status]
			FROM	
				ApplicationDetails
				LEFT JOIN ApplicationBuildingDetails ON ApplicationDetails.BuildingDetailsId = ApplicationBuildingDetails.Id
			WHERE 
				[ApplicationDetails].[Id] = @ApplicationId
		END
END