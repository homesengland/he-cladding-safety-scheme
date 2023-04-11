CREATE PROCEDURE [dbo].[InsertOrUpdateCorrespondanceAddress]
	@UserId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(150)
AS
BEGIN
	DECLARE @AddressId AS UNIQUEIDENTIFIER
	DECLARE @CorrespondanceUserId AS UNIQUEIDENTIFIER
	SELECT @CorrespondanceUserId = Id FROM [UserCorrespondanceAddress] WHERE [UserCorrespondanceAddress].[UserId] = @UserId
	
	IF (@CorrespondanceUserId IS NULL)
		BEGIN
		
			SET @AddressId = NEWID()  

			INSERT INTO [Address] ([Id], [NameNumber], [AddressLine1], [AddressLine2], [City], [County], [Postcode])
			VALUES (
			@AddressId,
			@NameNumber,
			@AddressLine1,
			@AddressLine2,
			@City,
			@County,
			@Postcode)

			SET @CorrespondanceUserId = NEWID()  
			INSERT INTO [UserCorrespondanceAddress] ([Id], [UserId], [AddressId])
			VALUES (
			@CorrespondanceUserId,
			@UserId,
			@AddressId)
		END
	ELSE
		BEGIN
			UPDATE [Address] SET
				[Address].[NameNumber] = @NameNumber,
				[Address].[AddressLine1] = @AddressLine1,
				[Address].[AddressLine2] = @AddressLine2,
				[Address].[City] = @City,
				[Address].[County] = @County,
				[Address].[Postcode] = @Postcode				
			WHERE 
				[Address].[Id] = @CorrespondanceUserId
		END		
END

