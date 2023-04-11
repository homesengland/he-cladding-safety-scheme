DECLARE @ApplicationOtherSourcesPursuedType TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
)

INSERT INTO @ApplicationOtherSourcesPursuedType ([Id], [Type])
VALUES
	(1, 'Yes, I have exhausted all other routes of funding and am unable to claim any funding'),
	(2, 'Yes, I am in the process of pursuing other routes of funding'),
	(3, 'No, there are still other routes of funding that I could pursue')

MERGE INTO [dbo].[ApplicationOtherSourcesPursuedType] t
USING @ApplicationOtherSourcesPursuedType s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY Source THEN 
	DELETE;