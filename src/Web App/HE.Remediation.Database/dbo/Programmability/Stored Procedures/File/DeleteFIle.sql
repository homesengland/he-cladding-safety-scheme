CREATE PROCEDURE [dbo].[DeleteFile]
	@FileId UNIQUEIDENTIFIER,
	@Extension NVARCHAR(150) OUTPUT
AS
BEGIN
	SELECT @Extension = [Extension] FROM [dbo].[File] WHERE [Id] = @FileId

	DELETE FROM dbo.[File]
	WHERE [Id] = @FileId
END
