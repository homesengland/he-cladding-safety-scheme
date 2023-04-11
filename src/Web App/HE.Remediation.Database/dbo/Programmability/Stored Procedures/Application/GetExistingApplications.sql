CREATE PROCEDURE [dbo].[GetExistingApplications]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		ApplicationDetails.Id AS ApplicationId,
		ReferenceNumber AS ApplicationNumber, 
		ISNULL(ApplicationBuildingDetails.UniqueName, '') AS UniqueBuildingName,
		CreationDate AS DateCreated,
		StageId AS Stage,
		StatusId AS [Status],
		CAST (0 AS BIT) AS OpenTasks
	FROM	
		ApplicationDetails
		LEFT JOIN ApplicationBuildingDetails ON ApplicationDetails.BuildingDetailsId = ApplicationBuildingDetails.Id
	WHERE UserId = @UserId
	ORDER BY 
		ReferenceNumber
END

