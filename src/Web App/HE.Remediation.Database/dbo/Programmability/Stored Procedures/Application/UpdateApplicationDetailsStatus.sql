CREATE PROCEDURE [dbo].[UpdateApplicationDetailsStatus]
	@ApplicationId UNIQUEIDENTIFIER,
	@StatusId INT
AS
BEGIN
	UPDATE [dbo].[ApplicationDetails]
	SET [StatusId] = @StatusId
	WHERE [Id] = @ApplicationId
END
