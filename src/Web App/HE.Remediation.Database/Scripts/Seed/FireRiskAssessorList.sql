DECLARE @FireRiskAssessorList TABLE
(
	[Id] INT NOT NULL,
	[CompanyName] NVARCHAR(150) NOT NULL,
	[RegionsCovered] NVARCHAR(150) NOT NULL,
	[EmailAddress] NVARCHAR(150) NOT NULL,
	[Telephone] NVARCHAR(150) NOT NULL
);

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(1, 'Airey Miller Surveys Limited', 'East of England, London, South-East', 'mark.pratten@aireymiller.com', '0203 948 6900');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(2, 'Anstey Horne & Co Limited', 'All regions', 'AlexParryJones@ansteyhorne.co.uk', '0204 534 3130');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(3, 'Avalon Surveyors', 'East Midlands, West Midlands, East of England, London, South East, South West', 'rmuir@avalonsurveyors.com', '0124 520 6366');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(4, 'Bailey Partnership', 'All regions', 's.demuth@baileyp.co.uk', '0173 288 5835');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(5, 'BEFS Ltd', 'All regions', 'hello@befsltd.co.uk', '0203 633 6450 (London), 0113 418 0375 (Leeds)');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(6, 'Capital Property & Construction Consultants Ltd', 'London', 'sean.kelly@capitalpcc.co.uk', '0203 653 0900');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(7, 'Cladding Project Management', 'All regions', 'ben@cpm.limited', '0117 042 7876');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(8, 'Cognition Architecture', 'East Midlands, West Midlands, East of England, London, South East, South West', 'admin@cognition.london', '0208 003 0215');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(9, 'EWS1 Surveyors Limited', 'All regions', 'contact@ews1surveyor.co.uk', '0800 368 7683');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(10, 'FR Consultants Ltd', 'All regions', 'enquiries@frconsultants.co.uk', '0179 433 2456');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(11, 'Noviun Architects', 'East Midlands, West Midlands, East of England, London, South East, South West', 'c.watts@noviun.com', '0795 016 2866');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(12, 'Oakleaf Surveying Limited', 'All regions', 'michael.harris@theoakleafgroup.co.uk', '0160 464 3100');

INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(13, 'Thomasons', 'North East, North West, Yorkshire and The Humber, East Midlands, West Midlands, London', 'manchester@thomasons.co.uk', '0161 839 3993');
INSERT INTO @FireRiskAssessorList ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
VALUES
	(14, 'Vemco Consulting', 'All regions', 'info@vemcoconsulting.com', '0122 365 5570');

MERGE INTO [dbo].[FireRiskAssessorList] t
USING @FireRiskAssessorList s
ON t.[Id] = s.[Id]
WHEN MATCHED THEN
	UPDATE SET t.[CompanyName] = s.[CompanyName],
			   t.[RegionsCovered] = s.[RegionsCovered],
			   t.[EmailAddress] = s.[EmailAddress],
			   t.[Telephone] = s.[Telephone]
WHEN NOT MATCHED THEN
	INSERT ([Id], [CompanyName], [RegionsCovered], [EmailAddress], [Telephone])
	VALUES (s.[Id], s.[CompanyName], s.[RegionsCovered], s.[EmailAddress], s.[Telephone]);
