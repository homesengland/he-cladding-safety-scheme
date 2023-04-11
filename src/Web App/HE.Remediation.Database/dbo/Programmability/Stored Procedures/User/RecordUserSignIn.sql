CREATE PROCEDURE [dbo].[RecordUserSignIn]
    @UserId     UNIQUEIDENTIFIER,
    @LoginTime  DATETIME2(7),
	@UserAgent	NVARCHAR (150),
	@IPAddress	NVARCHAR (150)
AS
BEGIN

    UPDATE  [UserDetails]
    SET     [LastLoginTime] = @LoginTime,
            [LoginCount] = [LoginCount] + 1
    WHERE   [UserId] = @UserId

    INSERT INTO [UserLoginHistory]
    (
        [Id],
        [UserId],
        [LoginTime],
        [UserAgent],
        [IPAddress]
    )
    VALUES
    (    
        NEWID(),
        @UserId,
        @LoginTime,
        @UserAgent,
        @IPAddress
    )

END