CREATE PROCEDURE [dbo].[UpdateCompanyAddressByUserId]
	@UserId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(10)
AS
BEGIN

	UPDATE 
		A
	SET
		A.[NameNumber] = @NameNumber,
		A.[AddressLine1] = @AddressLine1,
		A.[AddressLine2] = @AddressLine2,
		A.[City] = @City,
		A.[County] = @County,
		A.[Postcode] = @Postcode
	FROM
		[Address] A
	INNER JOIN
		[CompanyAddress] CA ON CA.[AddressId] = A.[Id]
	INNER JOIN
        [CompanyUserRole] CUR ON CUR.[CompanyId] = CA.[CompanyId]
    WHERE
        CUR.[UserId] = @UserId

END
