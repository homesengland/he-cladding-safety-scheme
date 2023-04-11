CREATE PROCEDURE [dbo].[SetConfirmedNotViable]
	@ApplicationId UNIQUEIDENTIFIER,
	@IsConfirmedNotViable BIT
AS
BEGIN
	UPDATE re
	SET re.[IsConfirmedNotViable] = @IsConfirmedNotViable
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
