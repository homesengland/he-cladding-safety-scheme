CREATE PROCEDURE [dbo].[GetFreeholderCompanyOrIndividual]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		AREF.[ResponsibleEntityTypeId]
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
			INNER JOIN
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ARE.[FreeholderId] = AREF.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO
