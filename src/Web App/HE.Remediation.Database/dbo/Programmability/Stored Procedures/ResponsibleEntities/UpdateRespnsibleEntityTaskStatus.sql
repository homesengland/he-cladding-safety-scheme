CREATE PROCEDURE [dbo].[UpdateResponsibleEntityTaskStatus]
	@ApplicationId UNIQUEIDENTIFIER,
	@TaskStatusId INT
AS
BEGIN
	UPDATE
		[ApplicationResponsibleEntity]
	SET
		[TaskStatusId] = @TaskStatusId
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO