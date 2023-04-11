CREATE PROCEDURE [dbo].[UserHasCompanyAddress]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT 
		IIF(COUNT(1) > 0, 1, 0) AS [EntityExists]
	FROM
		[CompanyAddress] CA
	INNER JOIN
		[CompanyUserRole] CUR ON CUR.[CompanyId] = CA.[CompanyId]
	WHERE
		CUR.[UserId] = @UserId

END