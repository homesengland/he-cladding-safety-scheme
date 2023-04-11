CREATE PROCEDURE [dbo].[GetUserByAuth0UserId]
    @Auth0UserId NVARCHAR(150)
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
        [Auth0UserId] = @Auth0UserId
END