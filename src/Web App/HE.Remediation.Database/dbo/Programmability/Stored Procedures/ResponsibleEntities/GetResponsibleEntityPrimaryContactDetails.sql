
CREATE PROCEDURE [dbo].[GetResponsibleEntityPrimaryContactDetails]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ARE.[FirstName],
		ARE.[LastName],
		ARE.[EmailAddress],
		ARE.[ContactNumber],
		ARE.[OrganisationId] AS OrganisationType,
		ARE.[OrganisationSubTypeId] AS OrganisationSubType
	FROM
	    [ApplicationResponsibleEntity] ARE 
		INNER JOIN [ApplicationDetails] AD ON AD.ResponsibleEntityId = ARE.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END