
CREATE PROCEDURE [dbo].[GetFireRiskAssessmentAssessorDetails]
	@ApplicationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
	TOP 1 
	fraad.[Id], 
	fraad.[FireRiskAssessmentId], 
	[FirstName], 
	[LastName], 
	[CompanyName], 
	[CompanyNumber], 
	[EmailAddress], 
	[Telephone]
	FROM 
	[ApplicationFireRiskAssessmentAssessorDetails] fraad
	INNER JOIN [ApplicationFireRiskAssessment] fra
	ON fraad.[FireRiskAssessmentId]=fra.[Id]
	INNER JOIN [ApplicationDetails] ad
	ON ad.[FireRiskAssessmentId]=fra.[Id]
	WHERE 
	ad.[Id] = @ApplicationId	
END

