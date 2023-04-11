CREATE PROCEDURE [dbo].[GetCompanyAddressByUserId]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT TOP 1
	    A.[NameNumber],
        A.[AddressLine1],
        A.[AddressLine2],
        A.[City],
        A.[County],
        A.[Postcode]
    FROM
        [Address] A
    INNER JOIN
        [CompanyAddress] CA ON CA.[AddressId] = A.[Id]
    INNER JOIN
        [CompanyUserRole] CUR ON CUR.[CompanyId] = CA.[CompanyId]
    WHERE
        CUR.[UserId] = @UserId

END
