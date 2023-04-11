CREATE PROCEDURE [dbo].[ScaffoldFirstTimeUserData]
    @Auth0UserId NVARCHAR(150),
    @EmailAddress NVARCHAR(150),
    @NewUserId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN

    SET @NewUserId = NEWID();

    INSERT INTO [UserDetails]
    (
        [UserId],
        [Auth0UserId],
        [EmailAddress],
        [LoginCount]
    )
    VALUES
    (
        @NewUserId,
        @Auth0UserId,
        @EmailAddress,
        0
    );

END