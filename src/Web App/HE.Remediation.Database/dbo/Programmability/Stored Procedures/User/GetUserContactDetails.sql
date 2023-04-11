CREATE PROCEDURE [dbo].[GetUserContactDetails]
	@UserId UNIQUEIDENTIFIER
AS
	SELECT
        [UserId],
        [FirstName],
        [LastName],
        [ContactNumber],
        [EmailAddress]       
    FROM
        [UserDetails]
    WHERE
        [UserId] = @UserId
