CREATE PROCEDURE [dbo].[GetUserByUserId]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        [UserId],
        [Auth0UserId],
        [FirstName],
        [LastName],
        [ContactNumber],
        [EmailAddress],
        [Created],
        [ResponsibleEntityTypeId] AS [ResponsibleEntityType],
        [LastLoginTime],
        [LoginCount]	
    FROM
        [UserDetails]
    WHERE
        [UserId] = @UserId
END