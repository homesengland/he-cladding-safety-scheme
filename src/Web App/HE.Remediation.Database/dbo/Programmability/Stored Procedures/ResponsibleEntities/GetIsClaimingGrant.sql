CREATE PROCEDURE [dbo].[GetIsClaimingGrant]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		re.[IsClaimingGrant],
		re.[HasOwners]
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
