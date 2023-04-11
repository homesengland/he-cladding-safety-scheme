CREATE PROCEDURE [dbo].[GetFireRiskAppraisalForApplication]
	@ApplicationId uniqueidentifier
AS
	select 
	top 1
		f.[Id] as [FileId],
		f.[Name],
		f.[Extension],
		f.[Size]

	from [dbo].[ApplicationFireRiskAssessment] fra
	inner join [dbo].[ApplicationDetails] ad on [ad].[FireRiskAssessmentId] = [fra].[Id]
	inner join [dbo].[File] f on [fra].[FraewFileId] = [f].[Id]
	where [ad].[Id] = @ApplicationId

