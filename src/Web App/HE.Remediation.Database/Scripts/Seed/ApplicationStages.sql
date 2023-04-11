MERGE INTO [dbo].[ApplicationStage] as Target
USING (
	VALUES
	(1, 'Apply For Grant'),
	(2, 'Sign Grant Funding Agreement'),
	(3, 'Add Works Plan'),
	(4, 'Works Started'),
	(5, 'Works Completed'),
	(6, 'Variation')
)
AS Source ([Id], [Type])
	ON Target.[Id] = Source.[Id]
WHEN MATCHED THEN 
	UPDATE SET [Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Type])
	VALUES ([Id], [Type])
WHEN NOT MATCHED BY Source THEN 
	DELETE;

GO