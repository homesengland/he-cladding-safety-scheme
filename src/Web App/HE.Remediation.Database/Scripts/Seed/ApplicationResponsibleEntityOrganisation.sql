DECLARE @ApplicationResponsibleEntityOrganisation TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
);

INSERT INTO @ApplicationResponsibleEntityOrganisation ([Id], [Type])
VALUES
	(1, 'Private company'),
	(2, 'Right-to-Manage company'),
	(3, 'Resident-led organisation'),
	(4, 'Registered Provider'),
	(5, 'Local Authority'),
	(6, 'Other');

MERGE INTO [dbo].[ApplicationResponsibleEntityOrganisation] t
USING @ApplicationResponsibleEntityOrganisation s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;