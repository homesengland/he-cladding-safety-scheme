CREATE PROCEDURE [InsertBuildingAddress]
	@AddressId UNIQUEIDENTIFIER,
	@NameNumber NVARCHAR(150),
	@AddressLine1 NVARCHAR(150),
	@AddressLine2 NVARCHAR(150),
	@City NVARCHAR(150),
	@County NVARCHAR(150),
	@Postcode NVARCHAR(10),
	@LocalAuthority NVARCHAR(150)
AS
BEGIN
	INSERT INTO [dbo].[Address]
		(
		[Id]
		,[NameNumber]
		,[AddressLine1]
		,[AddressLine2]
		,[City]
		,[County]
		,[Postcode]
		,[LocalAuthority]
		)
		 VALUES
		(
			@AddressId,
			@NameNumber,
			@AddressLine1,
			@AddressLine2,
			@City,
			@County,
			@Postcode,
			@LocalAuthority
		)
END
GO