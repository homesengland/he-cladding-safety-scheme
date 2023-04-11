CREATE PROCEDURE [dbo].[InsertCompanyAddressForUserId]
	@UserId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(10),
	@LocalAuthority NVARCHAR(150)
AS
BEGIN

	DECLARE @NewAddressId UNIQUEIDENTIFIER = NEWID();

	BEGIN TRANSACTION;

	INSERT INTO [Address]
	(
		[Id],
		[NameNumber],
		[AddressLine1],
		[AddressLine2],
		[City],
		[County],
		[Postcode],
		[LocalAuthority]
	)
	VALUES
	(
		@NewAddressId,
		@NameNumber,
		@AddressLine1,
		@AddressLine2,
		@City,
		@County,
		@Postcode,
		@LocalAuthority
	)

	INSERT INTO [CompanyAddress]
	(
		[AddressId],
		[CompanyId]
	)
	SELECT TOP 1 
		@NewAddressId, 
		CUR.[CompanyId]
	FROM
		[CompanyUserRole] CUR
	WHERE
		CUR.[UserId] = @UserId

	COMMIT TRANSACTION;

END
