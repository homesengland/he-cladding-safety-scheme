CREATE PROCEDURE [dbo].[GetHasOwners]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		re.[HasOwners],
		re.[SharedOwnerCount]
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
