CREATE PROCEDURE [dbo].[GetResponsibleEntityCompanyDetails]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 1
		er.[CompanyName],
		er.CompanyRegistrationNumber
	FROM
		[dbo].[ApplicationResponsibleEntity] er
			INNER JOIN
		[dbo].[ApplicationDetails] ad
			ON er.[Id] = ad.[ResponsibleEntityId]
	WHERE
		ad.[Id] = @ApplicationId
END