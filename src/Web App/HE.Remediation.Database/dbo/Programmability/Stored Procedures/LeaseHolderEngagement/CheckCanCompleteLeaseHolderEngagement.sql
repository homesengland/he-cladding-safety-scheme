CREATE PROCEDURE [dbo].[CheckCanCompleteLeaseHolderEngagement]
	@ApplicationId uniqueidentifier
AS
	select case when exists(
		select 
			lhef.LeaseHolderEngagementId
		from [dbo].[ApplicationLeaseHolderEngagementFile] lhef
		inner join [dbo].[ApplicationDetails] ad  on [ad].[LeaseHolderEngagementId] = [lhef].[LeaseHolderEngagementId]
		where [ad].[Id] = @ApplicationId)
	then 1 else 0 end
