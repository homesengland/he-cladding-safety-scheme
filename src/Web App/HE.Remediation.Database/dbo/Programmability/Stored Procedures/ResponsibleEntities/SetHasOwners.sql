CREATE PROCEDURE [dbo].[SetHasOwners]
	@ApplicationId UNIQUEIDENTIFIER,
	@HasOwners BIT,
	@SharedOwnerCount INT
AS
BEGIN
	UPDATE re
	SET 
		re.[HasOwners] = @HasOwners,
		re.[SharedOwnerCount] = @SharedOwnerCount
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
