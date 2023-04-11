MERGE INTO [dbo].[TaskStatus] as Target
USING (
	VALUES
	(1, 'Not Started'),
	(2, 'In Progress'),
	(3, 'Completed')
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