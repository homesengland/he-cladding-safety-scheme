
CREATE PROCEDURE [dbo].[InsertOrUpdateSecondaryContactDetails]
	@UserId UNIQUEIDENTIFIER,
	@Name NVARCHAR(150),
	@ContactNumber NVARCHAR(150),
	@EmailAddress NVARCHAR(150)
AS
BEGIN
	DECLARE @SecondaryUserId AS UNIQUEIDENTIFIER
	SELECT @SecondaryUserId = Id FROM [SecondaryContact] WHERE [SecondaryContact].[UserId] = @UserId
	
	IF (@SecondaryUserId IS NULL)
		BEGIN

			SET @SecondaryUserId = NEWID()  
			INSERT INTO [SecondaryContact] ([Id], [UserId], [CompanyId], [Name], [ContactNumber], [EmailAddress])
			VALUES (
			@SecondaryUserId,
			@UserId,
			NULL,
			@Name,
			@ContactNumber,
			@EmailAddress)			
			-- what else should be updated here?
		END
	ELSE
		BEGIN
			UPDATE [SecondaryContact] SET
				[SecondaryContact].[Name] = @Name,
				[SecondaryContact].[ContactNumber] = @ContactNumber,
				[SecondaryContact].[EmailAddress] = @EmailAddress
			WHERE 
				[SecondaryContact].[Id] = @SecondaryUserId
		END
END