CREATE PROCEDURE [dbo].[UserHasCompanyDetails]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT 
		IIF(COUNT(1) > 0, 1, 0) AS [EntityExists]
	FROM
		[CompanyDetails] CD
	INNER JOIN
		[CompanyUserRole] CUR ON CUR.[CompanyId] = CD.[Id]
	WHERE
		CUR.[UserId] = @UserId

END