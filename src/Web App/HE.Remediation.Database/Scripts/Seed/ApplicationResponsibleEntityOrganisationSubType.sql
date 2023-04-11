DECLARE @ApplicationResponsibleEntityOrganisationSubType TABLE
(
	Id INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
);

INSERT INTO @ApplicationResponsibleEntityOrganisationSubType ([Id], [Type])
VALUES
	(1, 'Individual'),
	(2, 'Private Trust'),
	(3, 'Other company type');

MERGE INTO [dbo].[ApplicationResponsibleEntityOrganisationSubType] t
USING @ApplicationResponsibleEntityOrganisationSubType s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;