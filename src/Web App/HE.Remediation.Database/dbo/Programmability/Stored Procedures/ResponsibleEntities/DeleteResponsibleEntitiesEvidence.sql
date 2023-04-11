CREATE PROCEDURE [dbo].[DeleteResponsibleEntitiesEvidence]
	@FileId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [dbo].[ApplicationResponsibleEntityFile]
	WHERE [FileId] = @FileId
END
