CREATE PROCEDURE UpdateFreeholderCompanyDetails
	@ApplicationId UNIQUEIDENTIFIER,
	@CompanyName NVARCHAR(150),
	@CompanyRegistrationNumber  NVARCHAR(150),
	@FirstName  NVARCHAR(150),
	@LastName  NVARCHAR(150),
	@EmailAddress  NVARCHAR(150),
	@ContactNumber  NVARCHAR(150)
AS
BEGIN
	UPDATE
		[ApplicationResponsibleEntityFreeholder]
	SET
		[CompanyName] = @CompanyName,
		[CompanyRegistrationNumber] = @CompanyRegistrationNumber,
		[FirstName] = @FirstName,
		[LastName] = @LastName,
		[EmailAddress] = @EmailAddress,
		[ContactNumber] =@ContactNumber
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
			INNER JOIN
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ARE.[FreeholderId] = AREF.[Id]
	WHERE
		AD.[Id] = @ApplicationId;
END
GO