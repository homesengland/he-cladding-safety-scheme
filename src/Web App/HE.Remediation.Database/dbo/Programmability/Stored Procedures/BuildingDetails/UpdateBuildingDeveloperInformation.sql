CREATE PROCEDURE [dbo].[UpdateBuildingDeveloperInformation]
	@ApplicationId UNIQUEIDENTIFIER,
	@DoYouKnowOriginalDeveloper BIT,
	@OrganisationName NVARCHAR(150),
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@Postcode NVARCHAR(10)
AS
BEGIN
	BEGIN TRANSACTION

	DECLARE @BuildingDetailsId UNIQUEIDENTIFIER = (
		SELECT TOP 1
			bd.[Id]
		FROM [dbo].[ApplicationBuildingDetails] bd
			INNER JOIN [dbo].[ApplicationDetails] ad
			ON bd.[Id] = ad.[BuildingDetailsId]
		WHERE ad.[Id] = @ApplicationId
	)

	UPDATE [dbo].[ApplicationBuildingDetails]
	SET [OriginalDeveloperKnown] = @DoYouKnowOriginalDeveloper
	WHERE [Id] = @BuildingDetailsId

	IF (@DoYouKnowOriginalDeveloper = 1)
	BEGIN
		DECLARE @DeveloperId UNIQUEIDENTIFIER = (
			SELECT TOP 1
				bd.[DeveloperId]
			FROM [dbo].[ApplicationBuildingDetails] bd
			WHERE bd.[Id] = @BuildingDetailsId
		)

		SET @DeveloperId = ISNULL(@DeveloperId, NEWID())

		MERGE INTO [dbo].[ApplicationDeveloper] t
		USING(
			VALUES(@DeveloperId, @OrganisationName)
		) s([Id], [Name])
		ON t.[Id] = s.[Id]
		WHEN MATCHED THEN
			UPDATE SET t.[Name] = s.[Name]
		WHEN NOT MATCHED THEN
			INSERT ([Id], [Name])
			VALUES (s.[Id], s.[Name]);

		UPDATE [dbo].[ApplicationBuildingDetails]
		SET [DeveloperId] = @DeveloperId
		WHERE [Id] = @BuildingDetailsId

		DECLARE @AddressId UNIQUEIDENTIFIER = (
			SELECT TOP 1
				dev.[AddressId]
			FROM [dbo].[ApplicationDeveloper] dev
			WHERE dev.[Id] = @DeveloperId
		)

		SET @AddressId = ISNULL(@AddressId, NEWID())

		MERGE INTO [dbo].[Address] t
		USING(
			VALUES(@AddressId, @NameNumber, @AddressLine1, @AddressLine2, @City, @Postcode)
		) s([Id], [NameNumber], [AddressLine1], [AddressLine2], [City], [Postcode])
		ON t.[Id] = s.[Id]
		WHEN MATCHED THEN
			UPDATE SET
			t.[NameNumber] = s.[NameNumber],
			t.[AddressLine1] = s.[AddressLine1],
			t.[AddressLine2] = s.[AddressLine2],
			t.[City] = s.[City],
			t.[Postcode] = s.[Postcode]
		WHEN NOT MATCHED THEN
			INSERT ([Id], [NameNumber], [AddressLine1], [AddressLine2], [City], [Postcode])
			VALUES (s.[Id], s.[NameNumber], s.[AddressLine1], s.[AddressLine2], s.[City], s.[Postcode]);

		UPDATE [dbo].[ApplicationDeveloper]
		SET [AddressId] = @AddressId
		WHERE [Id] = @DeveloperId
	END
	ELSE
		BEGIN
			DECLARE @DeveloperId2 UNIQUEIDENTIFIER = (
				SELECT TOP 1
					bd.[DeveloperId]
				FROM [dbo].[ApplicationBuildingDetails] bd
				WHERE bd.[Id] = @BuildingDetailsId
			)

			IF @DeveloperId2 IS NOT NULL
				UPDATE
					ApplicationBuildingDetails
				SET
					[DeveloperId] = NULL,
					[NameOfDevelopment] = NULL,
					[OriginalDeveloperKnown] = NULL
				WHERE
					[Id] = @BuildingDetailsId

				DELETE FROM ApplicationDeveloper WHERE [Id] = @DeveloperId2
		END

	COMMIT TRANSACTION
END
GO


