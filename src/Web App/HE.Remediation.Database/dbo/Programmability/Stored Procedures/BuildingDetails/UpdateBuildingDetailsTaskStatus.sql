CREATE PROCEDURE UpdateBuildingDetailsTaskStatus
	@ApplicationId UNIQUEIDENTIFIER,
	@TaskStatusId INT
AS
BEGIN
	UPDATE
		[ApplicationBuildingDetails]
	SET
		[TaskStatusId] = @TaskStatusId
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationBuildingDetails] ABD
			ON AD.[BuildingDetailsId] = ABD.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END