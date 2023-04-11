DECLARE @ApplicationDeveloperInBusinessType TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
)

INSERT INTO @ApplicationDeveloperInBusinessType ([Id], [Type])
VALUES
	(1, 'No'),
	(2, 'Yes'),
	(3, 'I don''t know')

MERGE INTO [dbo].[ApplicationDeveloperInBusinessType] t
USING @ApplicationDeveloperInBusinessType s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY Source THEN 
	DELETE;