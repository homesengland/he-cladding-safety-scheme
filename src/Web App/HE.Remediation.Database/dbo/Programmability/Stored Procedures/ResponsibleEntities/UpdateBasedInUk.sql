CREATE PROCEDURE [dbo].[UpdateBasedInUk]
	@ApplicationId UNIQUEIDENTIFIER,
	@IsBasedInUk BIT
AS
BEGIN
	UPDATE re
	SET re.IsRepresentativeUkBased = @IsBasedInUk
	FROM
		[dbo].[ApplicationResponsibleEntity] re
			INNER JOIN
		[dbo].[ApplicationDetails] ad
			ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END