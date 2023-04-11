CREATE PROCEDURE [dbo].[SetResponsibleEntityUkRegistered]
	@ApplicationId UNIQUEIDENTIFIER,
	@UkRegistered BIT
AS
BEGIN
	UPDATE re 
	SET re.[UkRegistered] = @UkRegistered
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
