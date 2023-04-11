CREATE PROCEDURE [dbo].[SetRepresentationCompanyOrIndividualAddress]
	@ApplicationId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(10)
AS
BEGIN
	BEGIN TRANSACTION

		DECLARE @ResponsibleEntityDetailsId UNIQUEIDENTIFIER = (
			SELECT TOP 1
				red.[Id]
			FROM [dbo].[ApplicationRepresentationEntityDetails] red
				INNER JOIN [dbo].[ApplicationResponsibleEntity] re
				ON red.[Id] = re.[RepresentationEntityDetailsId]
				INNER JOIN [dbo].[ApplicationDetails] ad
				ON re.[Id] = ad.[ResponsibleEntityId]
			WHERE ad.[Id] = @ApplicationId
		)

		DECLARE @AddressId UNIQUEIDENTIFIER = (
			SELECT TOP 1
				addr.[Id]
			FROM [dbo].[ApplicationRepresentationEntityDetails] red
				INNER JOIN [dbo].[Address] addr
				ON red.[AddressId] = addr.[Id]
			WHERE red.[Id] = @ResponsibleEntityDetailsId
		)

		SET @AddressId = ISNULL(@AddressId, NEWID())

		MERGE INTO [dbo].[Address] t
		USING(
			VALUES(@AddressId, @NameNumber, @AddressLine1, @AddressLine2, @City, @County, @Postcode)
		) s([Id], [NameNumber], [AddressLine1], [AddressLine2], [City], [County], [Postcode])
		ON t.[Id] = s.[Id]
		WHEN MATCHED THEN
			UPDATE SET
				t.[NameNumber] = s.[NameNumber],
				t.[AddressLine1] = s.[AddressLine1],
				t.[AddressLine2] = s.[AddressLine2],
				t.[City] = s.[City],
				t.[County] = s.[County],
				t.[Postcode] = s.[Postcode]
		WHEN NOT MATCHED THEN
			INSERT ([Id], [NameNumber], [AddressLine1], [AddressLine2], [City], [County], [Postcode])
			VALUES (s.[Id], s.[NameNumber], s.[AddressLine1], s.[AddressLine2], s.[City], s.[County], s.[Postcode]);

		UPDATE [dbo].[ApplicationRepresentationEntityDetails]
		SET [AddressId] = @AddressId
		WHERE [Id] = @ResponsibleEntityDetailsId AND [AddressId] IS NULL

	COMMIT TRANSACTION
END
