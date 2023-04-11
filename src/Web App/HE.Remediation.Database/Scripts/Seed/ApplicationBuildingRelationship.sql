DECLARE @BuildingRelationship TABLE
(
	[Id] INT NOT NULL,
	[Type] NVARCHAR(150) NOT NULL
);

INSERT INTO @BuildingRelationship ([Id], [Type])
VALUES
	(1, 'Freeholder'),
	(2, 'Head Leaseholder'),
	(3, 'Right-to-Manage Company');

MERGE INTO [dbo].[ApplicationBuildingRelationship] t
USING @BuildingRelationship s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[Type] = s.[Type]
WHEN NOT MATCHED THEN
	INSERT ([Id], [Type])
	VALUES (s.[Id], s.[Type]);