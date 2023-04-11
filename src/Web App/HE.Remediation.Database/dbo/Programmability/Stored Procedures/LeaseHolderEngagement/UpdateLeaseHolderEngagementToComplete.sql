CREATE PROCEDURE [dbo].[UpdateLeaseHolderEngagementToComplete]
	@ApplicationId uniqueidentifier
AS
	update dbo.ApplicationLeaseHolderEngagement
	set TaskStatusId = 3
	where Id = (select LeaseHolderEngagementId from dbo.ApplicationDetails where Id = @ApplicationId)
