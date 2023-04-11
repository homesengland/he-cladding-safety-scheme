CREATE PROCEDURE [dbo].[UpdateResponsibleEntityCompanyType]
	@ApplicationId UNIQUEIDENTIFIER,
	@OrganisationTypeId INT,
	@OrganisationSubTypeId INT,
	@OrganisationSubTypeDescription NVARCHAR(1000)
AS
BEGIN
	UPDATE re
	SET 
		re.[OrganisationId] = @OrganisationTypeId,
		re.[OrganisationSubTypeId] = @OrganisationSubTypeId,
		re.[OrganisationSubTypeDescription] = @OrganisationSubTypeDescription
	FROM [dbo].[ApplicationResponsibleEntity] re
		INNER JOIN [dbo].[ApplicationDetails] ad
		ON re.[Id] = ad.[ResponsibleEntityId]
	WHERE ad.[Id] = @ApplicationId
END
