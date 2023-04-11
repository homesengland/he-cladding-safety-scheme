CREATE PROCEDURE [dbo].[SetUserResponsibleEntityTypeByUserId]
	@UserId UNIQUEIDENTIFIER,
	@ResponsibleEntityTypeId INT
AS
BEGIN

	UPDATE
		[UserDetails]
	SET
		[ResponsibleEntityTypeId] = @ResponsibleEntityTypeId
	WHERE
		[UserId] = @UserId

END
