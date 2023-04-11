CREATE PROCEDURE [dbo].[GetResponsibleEntityAnswers]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		AD.ReferenceNumber,
		AD.[Id] AS 'ApplicationId',
		ARE.[Id] AS 'ResponsibleEntityId',
		ARE.[RepresentationTypeId],
		ARE.BuildingRelationshipId,
		ARED.[ResponsibleEntityTypeId] AS 'RepresentativeResponsibleEntityTypeId',
		ARE.[IsRepresentativeUkBased] AS 'RepresentativeUKBased',
		ARET1.[Type] AS 'RepresentativeIndividualOrCompany',
		CONCAT_WS(',', ARED.[CompanyName], ARED.[CompanyRegistration]) AS 'RepresentativeCompanyDetails',
		CONCAT_WS(',', CONCAT_WS(' ', ARED.[FirstName], ARED.[LastName]), ARED.[ContactNumber], ARED.[EmailAddress]) AS 'RepresentativeDetails',
		CONCAT_WS(',', ADDR.[NameNumber], ADDR.[AddressLine1], ADDR.[AddressLine2], ADDR.[City], ADDR.[County], ADDR.[Postcode]) AS 'RepresentativeAddress',
		ARE.[BuildingRelationshipId] AS 'ResponsibleEntityRelationId',
		ABR.[Type] AS 'ResponsibleEntityRelation',
		ARE.[OrganisationId] AS 'ResponsibleEntityCompanyTypeId',
		CONCAT_WS(' / ', AREO.[Type], AREOST.[Type], ARE.[OrganisationSubTypeDescription]) AS 'ResponsibleEntityCompanyType',
		ARE.[OrganisationSubTypeId] AS 'ResponsibleEntityCompanySubTypeId',
		ARE.[UkRegistered] AS 'ResponsibleEntityRegisteredInUK',
		ARE.[CompanyName] + ',' + ARED.[CompanyRegistration] AS 'ResponsibleEntityCompanyDetails',
		CONCAT_WS(',', CONCAT_WS(' ', ARE.[FirstName], ARE.[LastName]), ARE.[ContactNumber], ARE.[EmailAddress]) AS 'ResponsibleEntityPrimaryContact',
		CONCAT_WS(',', ADDR2.[NameNumber], ADDR2.[AddressLine1], ADDR2.[AddressLine2], ADDR2.[City], ADDR2.[County], ADDR2.[Postcode]) AS 'ResponsibleEntityCompanyAddress',
		ARE.[SharedOwnerCount] AS 'ResponsibleEntitySharedOwners',
		ARE.[IsClaimingGrant] AS 'ResponsibleEntityClaimingGrant',
		ARE.[IsConfirmedNotViable] AS 'ConfirmedNotViable',
		AREF.[Id] AS 'FreeholderId',
		AREF.[ResponsibleEntityTypeId] AS 'FreeholderResponsibleEntityTypeId',
		ARET2.[Type] AS 'FreeholderIndividualOrCompany',
		CONCAT_WS(',', AREF.[CompanyName], AREF.[CompanyRegistrationNumber]) AS 'FreeholderCompanyDetails',
		CONCAT_WS(',', CONCAT_WS(' ', AREF.[FirstName], AREF.[LastName]), AREF.[ContactNumber], AREF.[EmailAddress]) AS 'FreeholderDetails',
		CONCAT_WS(',', ADDR3.[NameNumber], ADDR3.[AddressLine1], ADDR3.[AddressLine2], ADDR3.[City], ADDR3.[County], ADDR3.[Postcode]) AS 'FreeholderAddress'
	FROM
		[ApplicationDetails] AD
			LEFT JOIN
		[ApplicationResponsibleEntity] ARE
			ON AD.[ResponsibleEntityId] = ARE.[Id]
			LEFT JOIN
		ApplicationRepresentationType ART
			ON ARE.[RepresentationTypeId] = ART.[Id]
			LEFT JOIN
		[ApplicationBuildingRelationship] ABR
			ON ARE.[BuildingRelationshipId] = ABR.[Id]
			LEFT JOIN
		ApplicationResponsibleEntityOrganisation AREO
			ON ARE.[OrganisationId] = AREO.[Id]
			LEFT JOIN
		ApplicationResponsibleEntityOrganisationSubType AREOST
			ON ARE.[OrganisationSubTypeId] = AREOST.[Id]
			LEFT JOIN
		[ApplicationRepresentationEntityDetails] ARED
			ON ARE.[RepresentationEntityDetailsId] = ARED.[Id]
			LEFT JOIN
		[ApplicationResponsibleEntityFreeholder] AREF
			ON ARE.[FreeholderId] = AREF.[Id]
			LEFT JOIN
		ApplicationResponsibleEntityType ARET1
			ON ARED.[ResponsibleEntityTypeId] = ARET1.[Id]
			LEFT JOIN
		ApplicationResponsibleEntityType ARET2
			ON AREF.[ResponsibleEntityTypeId] = ARET2.[Id]
			LEFT JOIN
		[Address] ADDR
			ON ARED.AddressId = ADDR.Id
			LEFT JOIN
		[Address] ADDR2
			ON ARE.AddressId = ADDR2.Id
			LEFT JOIN
		[Address] ADDR3
			ON ARE.AddressId = ADDR3.Id
	WHERE
		AD.[Id] = @ApplicationId
END
GO