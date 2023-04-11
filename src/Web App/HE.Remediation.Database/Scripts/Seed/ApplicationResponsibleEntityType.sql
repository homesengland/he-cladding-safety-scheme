DECLARE @ResponsibleEntityType TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
);

INSERT INTO @ResponsibleEntityType ([Id], [Type])
VALUES
	(1, 'Company'),
	(2, 'Individual');

MERGE INTO [dbo].[ApplicationResponsibleEntityType] t
USING @ResponsibleEntityType s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED THEN
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type]);