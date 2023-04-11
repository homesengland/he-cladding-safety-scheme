CREATE PROCEDURE [dbo].[UpdateRepresentationCompanyOrIndividualDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@CompanyName NVARCHAR(150),
	@CompanyRegistration NVARCHAR(150),
	@FirstName NVARCHAR(150),
	@LastName NVARCHAR(150),
	@EmailAddress NVARCHAR(150),
	@ContactNumber NVARCHAR(150)
AS 
BEGIN
	UPDATE red
	SET 
		red.[CompanyName] = @CompanyName,
		red.[CompanyRegistration] = @CompanyRegistration,
		red.[FirstName] = @FirstName,
		red.[LastName] = @LastName,
		red.[EmailAddress] = @EmailAddress,
		red.[ContactNumber] = @ContactNumber
	FROM [dbo].[ApplicationRepresentationEntityDetails] red
		INNER JOIN [dbo].[ApplicationResponsibleEntity] re
		ON red.[Id] = re.[RepresentationEntityDetailsId]
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId

END