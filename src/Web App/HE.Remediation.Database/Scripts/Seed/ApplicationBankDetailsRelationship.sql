DECLARE @ApplicationBankDetailsRelationship TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
);

INSERT INTO @ApplicationBankDetailsRelationship
VALUES 
	(1, 'My Account'),
	(2, 'Responsible Entity Account')

MERGE INTO [dbo].[ApplicationBankDetailsRelationship] t
USING @ApplicationBankDetailsRelationship s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN 
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY Source THEN 
	DELETE;
