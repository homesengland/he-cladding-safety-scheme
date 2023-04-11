CREATE PROCEDURE [dbo].[UpdateCompanyDetailsByUserId]
	@UserId UNIQUEIDENTIFIER,
	@CompanyName NVARCHAR(150),
	@CompanyRegistrationNumber NVARCHAR(150),
	@UserRoleInCompany NVARCHAR(150)
AS
BEGIN

	BEGIN TRANSACTION;

	UPDATE
		CD
	SET
		CD.[Name] = @CompanyName,
		CD.[RegistrationNumber] = @CompanyRegistrationNumber
	FROM
		[CompanyDetails] CD
	INNER JOIN
		[CompanyUserRole] CUR ON CUR.[CompanyId] = CD.[Id]
	WHERE
		CUR.[UserId] = @UserId

	UPDATE
		CUR
	SET
		CUR.[Role] = @UserRoleInCompany
	FROM
		[CompanyUserRole] CUR
	WHERE
		CUR.[UserId] = @UserId

	COMMIT TRANSACTION;

END