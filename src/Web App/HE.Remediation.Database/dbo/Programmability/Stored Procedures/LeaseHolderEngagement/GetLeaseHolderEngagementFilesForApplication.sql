CREATE PROCEDURE [dbo].[GetLeaseHolderEngagementFilesForApplication]
	@ApplicationId uniqueidentifier
AS
	select 
		f.[Id],
		f.[Name],
		f.[Extension],
		f.[Size]

	from [dbo].[ApplicationLeaseHolderEngagementFile] alhef
	inner join [dbo].[ApplicationDetails] ad on [ad].[LeaseHolderEngagementId] = [alhef].[LeaseHolderEngagementId]
	inner join [dbo].[File] f on [alhef].[FileId] = [f].[Id]
	where [ad].[Id] = @ApplicationId
