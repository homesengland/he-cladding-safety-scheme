
CREATE PROCEDURE [dbo].[UpdateResponsibleEntityPrimaryContactDetails]
	@ApplicationId UNIQUEIDENTIFIER,
	@FirstName  NVARCHAR(150),
	@LastName  NVARCHAR(150),
	@EmailAddress  NVARCHAR(150),
	@ContactNumber  NVARCHAR(150)
AS
BEGIN
	UPDATE
		[ApplicationResponsibleEntity]
	SET
		[FirstName] = @FirstName,
		[LastName] = @LastName,
		[EmailAddress] = @EmailAddress,
		[ContactNumber] = @ContactNumber
	FROM
		[ApplicationDetails] AD
		INNER JOIN [ApplicationResponsibleEntity] ARE ON AD.[ResponsibleEntityId] = ARE.[Id]
	WHERE
		AD.[Id] = @ApplicationId;
END