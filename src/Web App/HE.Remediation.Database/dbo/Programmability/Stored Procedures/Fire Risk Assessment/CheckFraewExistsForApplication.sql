CREATE PROCEDURE [dbo].[CheckFraewExistsForApplication]
	@ApplicationId uniqueidentifier
AS
	select case when exists(
		select 
			afra.FraewFileId
		from dbo.ApplicationFireRiskAssessment afra
		inner join dbo.ApplicationDetails ad on ad.FireRiskAssessmentId = afra.Id
		where ad.Id = @ApplicationId and afra.FraewFileId IS NOT NULL)
	then 1 else 0 end
