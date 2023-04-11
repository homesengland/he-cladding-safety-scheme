DECLARE @ApplicationFundingRoutesType TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
)

INSERT INTO @ApplicationFundingRoutesType ([Id], [Type])
VALUES
	(1, 'Developer that has signed up to the Developer’s pledge'),
	(2, 'Developer that has not signed up to the pledge, or freeholder'),
	(3, 'Warranty, insurance or other claim against contractor or design team'),
	(4, 'Other')

MERGE INTO [dbo].[ApplicationFundingRoutesType] t
USING @ApplicationFundingRoutesType s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type])
WHEN NOT MATCHED BY Source THEN 
	DELETE;