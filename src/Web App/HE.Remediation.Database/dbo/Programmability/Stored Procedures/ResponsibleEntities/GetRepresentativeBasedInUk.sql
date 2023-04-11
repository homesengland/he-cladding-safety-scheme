CREATE PROCEDURE [dbo].[GetRepresentativeBasedInUk]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		re.[IsRepresentativeUkBased]
	FROM
		[dbo].[ApplicationResponsibleEntity] re
			INNER JOIN
		[dbo].[ApplicationDetails] ad
			ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END