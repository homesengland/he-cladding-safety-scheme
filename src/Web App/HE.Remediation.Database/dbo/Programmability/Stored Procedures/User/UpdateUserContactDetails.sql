CREATE PROCEDURE [dbo].[UpdateUserContactDetails]
	@UserId UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(150),
	@LastName NVARCHAR(150),
	@ContactNumber NVARCHAR(150)
AS
	DECLARE @ExistingUserId AS UNIQUEIDENTIFIER
		
	SELECT 
	@ExistingUserId = [UserId]
	FROM [UserDetails]
	WHERE 
	[UserId]=@UserId

	IF (@ExistingUserId IS NULL)
	BEGIN			
		RETURN -1	
	END

	UPDATE 
	[UserDetails] 
	SET 
	[UserDetails].[FirstName] = @FirstName,
	[UserDetails].[LastName] = @LastName,
	[UserDetails].[ContactNumber] = @ContactNumber
	WHERE 
	[UserDetails].[UserId] = @UserId
