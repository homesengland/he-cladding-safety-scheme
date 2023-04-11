CREATE PROCEDURE [dbo].[InsertCompanyDetailsForUserId]
	@UserId UNIQUEIDENTIFIER,
	@CompanyName NVARCHAR(150),
	@CompanyRegistrationNumber NVARCHAR(150),
	@UserRoleInCompany NVARCHAR(150)
AS
BEGIN

	DECLARE @CompanyId UNIQUEIDENTIFIER = NEWID();

	BEGIN TRANSACTION;

	INSERT INTO [CompanyDetails]
	(
		[Id],
		[Name],
		[RegistrationNumber]
	)
	VALUES
	(
		@CompanyId,
		@CompanyName,
		@CompanyRegistrationNumber
	)

	INSERT INTO [CompanyUserRole]
	(
		[CompanyId],
		[UserId],
		[Role]
	)
	VALUES
	(
		@CompanyId,
		@UserId,
		@UserRoleInCompany
	)

	COMMIT TRANSACTION;

END