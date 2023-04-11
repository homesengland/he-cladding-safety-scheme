







CREATE PROCEDURE [dbo].[GetFireRiskAssessorList]
AS
BEGIN
    SELECT
        Id,
        [CompanyName],
        [RegionsCovered],
        [EmailAddress],
        [Telephone]
    FROM
		[FireRiskAssessorList]
    ORDER BY
		[CompanyName]
END