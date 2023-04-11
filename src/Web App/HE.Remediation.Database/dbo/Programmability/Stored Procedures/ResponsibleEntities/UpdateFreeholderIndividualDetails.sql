CREATE PROCEDURE [dbo].[UpdateFreeholderIndividualDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@FirstName  NVARCHAR(150),
	@LastName  NVARCHAR(150),
	@EmailAddress  NVARCHAR(150),
	@ContactNumber  NVARCHAR(150)
AS
BEGIN
	UPDATE
		[ApplicationResponsibleEntityFreeholder]
	SET
		[CompanyName] = null,
		[CompanyRegistrationNumber] = null,
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