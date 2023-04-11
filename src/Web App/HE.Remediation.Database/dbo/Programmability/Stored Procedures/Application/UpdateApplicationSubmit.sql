



CREATE PROCEDURE [dbo].[UpdateApplicationSubmit]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @ApplicationStatusId AS INT 
	SELECT @ApplicationStatusId = Id FROM ApplicationStatus WHERE Type='Submitted'

    UPDATE [ApplicationDetails] SET StatusId = @ApplicationStatusId, Submitted = 1 WHERE Id = @ApplicationId
END