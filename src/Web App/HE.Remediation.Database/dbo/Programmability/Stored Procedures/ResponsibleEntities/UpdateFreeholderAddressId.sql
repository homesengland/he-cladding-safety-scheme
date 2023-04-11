CREATE PROCEDURE [dbo].[UpdateFreeholderAddressId]
    @ApplicationId UNIQUEIDENTIFIER,
    @AddressId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE
		[ApplicationResponsibleEntityFreeholder] 
	SET
		[AddressId] = @AddressId
	FROM
		[ApplicationResponsibleEntityFreeholder] AREF
			INNER JOIN
        [ApplicationResponsibleEntity] ARE
			ON AREF.[Id] = ARE.[FreeholderId]
			INNER JOIN
		[ApplicationDetails] AD
			ON ARE.[Id] = AD.[ResponsibleEntityId]
	WHERE
		AD.[Id] = @ApplicationId
END
GO