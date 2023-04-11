MERGE INTO [dbo].[ApplicationStatus] as Target
USING (
	VALUES
    (1, 'Not started'),
    (2, 'In progress'),
    (3, 'Submitted'),
    (4, 'In review'),
    (5, 'Approved'),
    (6, 'Not Eligible'),
    (7, 'Not signed'),
    (8, 'Signed'),
    (9, 'Counter-signed'),
    (10, 'Completed')
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