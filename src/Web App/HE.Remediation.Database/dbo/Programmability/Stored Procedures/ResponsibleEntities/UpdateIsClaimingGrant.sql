CREATE PROCEDURE [dbo].[UpdateIsClaimingGrant]
	@ApplicationId UNIQUEIDENTIFIER,
	@IsClaimingGrant BIT
AS
BEGIN
	UPDATE re
		SET re.[IsClaimingGrant] = @IsClaimingGrant
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
