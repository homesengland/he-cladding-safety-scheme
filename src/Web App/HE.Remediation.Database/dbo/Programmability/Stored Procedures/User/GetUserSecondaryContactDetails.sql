CREATE PROCEDURE [dbo].[GetUserSecondaryContactDetails]
	@UserId UNIQUEIDENTIFIER
AS
	SELECT
        [Name],
        [ContactNumber],
        [EmailAddress]       
    FROM
        [SecondaryContact]
    WHERE
        [UserId] = @UserId
