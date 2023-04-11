

CREATE PROCEDURE [dbo].[UpdateConfirmDeclaration]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [ApplicationDetails] SET ConfirmDeclaration = 1 WHERE Id = @ApplicationId
END