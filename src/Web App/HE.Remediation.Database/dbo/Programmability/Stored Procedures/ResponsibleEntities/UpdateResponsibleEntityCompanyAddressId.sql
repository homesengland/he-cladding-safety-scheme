CREATE PROCEDURE [dbo].[UpdateResponsibleEntityCompanyAddressId]
    @ApplicationId UNIQUEIDENTIFIER,
    @AddressId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE
		[ApplicationResponsibleEntity]
	SET
		[AddressId] = @AddressId
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO