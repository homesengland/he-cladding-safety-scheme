﻿CREATE PROCEDURE GetFreeholderCompanyDetails
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		AREF.[CompanyName],
		AREF.[CompanyRegistrationNumber],
		AREF.[FirstName],
		AREF.[LastName],
		AREF.[EmailAddress],
		AREF.[ContactNumber]
	FROM
		[ApplicationDetails] AD
			INNER JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.ResponsibleEntityId = ARE.[Id]
			INNER JOIN
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ARE.[FreeholderId] = AREF.[Id]
	WHERE
		AD.[Id] = @ApplicationId
END
GO