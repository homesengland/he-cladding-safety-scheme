CREATE PROCEDURE [dbo].[GetCompanyDetailsByUserId]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT TOP 1
		CD.[Name] AS [CompanyName],
		CD.[RegistrationNumber] AS [CompanyRegistrationNumber],
		CRU.[Role] AS [UserRoleInCompany]
	FROM
		[CompanyDetails] CD
	INNER JOIN
		[CompanyUserRole] CRU ON CRU.[CompanyId] = CD.[Id]
	WHERE
		CRU.[UserId] = @UserId

END