CREATE PROCEDURE [dbo].[UpdateResponsibleEntityCompanyDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@CompanyName NVARCHAR(150),
	@CompanyRegistrationNumber NVARCHAR(150)
AS
BEGIN
	UPDATE er
	SET
		er.[CompanyName] = @CompanyName,
		er.[CompanyRegistrationNumber] = @CompanyRegistrationNumber
	FROM
		[dbo].[ApplicationResponsibleEntity] er
			INNER JOIN
		[dbo].[ApplicationDetails] ad
			ON er.[Id] = ad.[ResponsibleEntityId]
	WHERE
		ad.[Id] = @ApplicationId
END