DECLARE @ApplicationRepresentationType TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
);

INSERT INTO @ApplicationRepresentationType
VALUES 
	(1, 'Responsible Entity'),
	(2, 'Representative')

MERGE INTO [dbo].[ApplicationRepresentationType] t
USING @ApplicationRepresentationType s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN 
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY Source THEN 
	DELETE;
