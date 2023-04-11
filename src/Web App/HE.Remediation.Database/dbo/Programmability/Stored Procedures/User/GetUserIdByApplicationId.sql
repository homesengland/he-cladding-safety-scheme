CREATE PROCEDURE [dbo].[GetUserIdByApplicationId]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ad.[UserId]
	FROM [dbo].[ApplicationDetails] ad
	WHERE ad.[Id] = @ApplicationId
END
