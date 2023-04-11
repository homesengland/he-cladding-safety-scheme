CREATE PROCEDURE [dbo].[UpdateFreeholderCompanyOrIndividual]
	@ApplicationId UNIQUEIDENTIFIER,
	@ResponsibleEntityTypeId INT
AS
BEGIN
	UPDATE
		[ApplicationResponsibleEntityFreeholder]
	SET
		[ResponsibleEntityTypeId] = @ResponsibleEntityTypeId
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
			INNER JOIN
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ARE.[FreeholderId] = AREF.[Id]
	WHERE
		AD.[Id] = @ApplicationId;
END
GO